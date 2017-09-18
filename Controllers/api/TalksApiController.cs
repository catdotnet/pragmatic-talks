using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PragmaticTalks.Data;
using PragmaticTalks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PragmaticTalks.Controllers
{
    [Produces("application/json")]
    [Route("api/talks")]
    public class TalksApiController : ApiController
    {
        private readonly PragmaticContext _context;

        public TalksApiController(PragmaticContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Talk> GetTalks()
        {
            return _context.Talks;
        }

        [HttpPost]
        public async Task<IActionResult> PostTalk([FromBody] TalkCreation request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (CurrentUser == null) return Forbidden();

            var tags = await _context.Tags.Where(t => request.Tags.Contains(t.Name)).ToListAsync();
            if (tags.Count > 3) return BadRequest(ModelState);

            var talk = new Talk
            {
                Title = request.Title,
                DateCreation = DateTime.UtcNow,
                Speaker = CurrentUser
            };
            talk.Tags = tags.Select(t => new TalkTag { Tag = t, Talk = talk }).ToList();

            _context.Talks.Add(talk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTalk", new { id = talk.Id }, talk);
        }

        // DELETE: api/TalksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTalk([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var talk = await _context.Talks.SingleOrDefaultAsync(m => m.Id == id);
            if (talk == null)
            {
                return NotFound();
            }

            _context.Talks.Remove(talk);
            await _context.SaveChangesAsync();

            return Ok(talk);
        }

        private bool TalkExists(int id)
        {
            return _context.Talks.Any(e => e.Id == id);
        }
    }
}