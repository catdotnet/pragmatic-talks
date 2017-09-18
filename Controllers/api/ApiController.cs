using Microsoft.AspNetCore.Mvc;
using PragmaticTalks.Data;
using System.Linq;
using System.Net;

namespace PragmaticTalks.Controllers
{
    public abstract class ApiController : Controller
    {
        private readonly PragmaticContext _context;
        private Speaker _currentUser;

        public ApiController(PragmaticContext context)
        {
            _context = context;
        }

        protected Speaker CurrentUser {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return null;
                if (_currentUser == null)
                {
                    var email = HttpContext.User.Identity.Name;
                    _currentUser = _context.Users.FirstOrDefault(u => u.Email == email);
                }

                return _currentUser;
            }
        }

        protected IActionResult Forbidden()
        {
            return new StatusCodeResult((int)HttpStatusCode.Forbidden);
        }
    }
}
