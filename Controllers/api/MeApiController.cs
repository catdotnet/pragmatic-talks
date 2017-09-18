using Microsoft.AspNetCore.Mvc;
using PragmaticTalks.Data;
using PragmaticTalks.Model;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetMeAsync()
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
