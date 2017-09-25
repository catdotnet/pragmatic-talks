using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PragmaticTalks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PragmaticTalks.Controllers
{
    public class AccountController : Controller
    {
        private const string GoogleApiKey = "AIzaSyCbx_yBJkDqmY5djoEK9EPrDLhbirwuSRI";
        private readonly UserManager<Speaker> _userManager;
        private readonly SignInManager<Speaker> _signInManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<Speaker> userManager, SignInManager<Speaker> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToLocal();
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToLocal();
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.Identity.Name;
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByEmailAsync(email);
                if (currentUser.IsBlocked)
                {
                    await _signInManager.SignOutAsync();
                    ErrorMessage = $"User is blocked";
                    return RedirectToLocal();
                }

                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }


            return await ExternalLoginConfirmation(name, email, returnUrl);
        }

        private async Task<IActionResult> ExternalLoginConfirmation(string name, string email, string returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToLocal();
            }

            var twitterName = info.Principal.Claims.Where(x => x.Type == "urn:twitter:screenname").Select(x => x.Value).FirstOrDefault();
            var userId = info.Principal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            var avatar = string.IsNullOrEmpty(twitterName) ? await GetGooglePictureAsync(userId) : await GetTwitterPictureAsync(twitterName);
            var user = new Speaker { UserName = email, Email = email, DisplayName = name, AvatarUrl = avatar };
            user.IsAdministrator = email == "fer.escolar@gmail.com";

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                    return RedirectToLocal(returnUrl);
                }
            }

            AddErrors(result);

            return RedirectToLocal(returnUrl);
        }

        private void AddErrors(IdentityResult result)
        {
            var sb = new System.Text.StringBuilder(ErrorMessage);
            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.Description);
            }

            ErrorMessage = sb.ToString();
        }

        private IActionResult RedirectToLocal(string returnUrl = null)
        {
            
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                if (!string.IsNullOrEmpty(ErrorMessage))
                    return RedirectToAction(nameof(HomeController.Index), "Home", new { errorMessage = ErrorMessage});

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private async Task<string> GetGooglePictureAsync(string userId)
        {
            var url = $"https://www.googleapis.com/plus/v1/people/{userId}?fields=image&key={GoogleApiKey}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                JObject result = JsonConvert.DeserializeObject<JObject>(json);
                return result["image"]["url"].Value<string>();
            }
        }

        private async Task<string> GetTwitterPictureAsync(string screenName)
        {
            var oAuthConsumerKey = "nKeG3ApWmQnyQ1WdCjwxYT3kG";
            var oAuthConsumerSecret = "MmSjuydKuDD0o3ZcIxAcq4l2yE1n35bVnY00r4vrNaMuquHs8m";
            var oAuthUrl = "https://api.twitter.com/oauth2/token";
            var avatarUrl = $"https://api.twitter.com/1.1/users/show.json?screen_name={screenName}";

            using (var client = new HttpClient())
            {
                var schema = "Basic";
                var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oAuthConsumerKey) + ":" + Uri.EscapeDataString((oAuthConsumerSecret))));
                var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                    { "grant_type", "client_credentials" }
                });
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schema, authHeader);
                var response = await client.PostAsync(oAuthUrl, content);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<JObject>(json);

                authHeader = obj["access_token"].Value<string>();
                schema = obj["token_type"].Value<string>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schema, authHeader);
                response = await client.GetAsync(avatarUrl);
                json = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<JObject>(json);

                return obj["profile_image_url"].Value<string>();
            }
        }
    }
}
