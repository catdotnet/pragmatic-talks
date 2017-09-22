using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PragmaticTalks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PragmaticTalks.Controllers
{
    [Produces("application/json")]
    [Route("api/speakers")]
    public class SpeakersApiController : ApiController
    {
        private readonly PragmaticContext _context;

        public SpeakersApiController(PragmaticContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Speaker> GetSpeakers(int number = 0)
        {
            IQueryable<Speaker> speakerQuery = _context.Users.Where(u => u.TalksCounter > 0).OrderBy(u => u.TalksCounter).ThenBy(u => u.DisplayName);
            if (number > 0)
                speakerQuery = speakerQuery.Take(number);

            return speakerQuery.ToList();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeaker([FromRoute] string id)
        {
            if (CurrentUser == null || !!CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var speaker = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (speaker == null) return NotFound();

            _context.Users.Remove(speaker);
            await _context.SaveChangesAsync();

            return Ok(speaker);
        }
    }
}