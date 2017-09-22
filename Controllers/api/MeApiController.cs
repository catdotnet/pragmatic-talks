using Microsoft.AspNetCore.Mvc;
using PragmaticTalks.Data;
using PragmaticTalks.Model;

namespace PragmaticTalks.Controllers
{
    [Produces("application/json")]
    [Route("api/me")]
    public class MeApiController : ApiController
    {
        private readonly PragmaticContext _context;

        public MeApiController(PragmaticContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMe()
        {
            if (CurrentUser == null) return Forbidden();

            return Ok(new MyInformation
            {
                Id = CurrentUser.Id,
                DisplayName = CurrentUser.DisplayName,
                Avatar = CurrentUser.AvatarUrl,
                Email = CurrentUser.Email,
                IsAdministrator = CurrentUser.IsAdministrator
            });
        }
    }
}
