using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PragmaticTalks.Model;

namespace PragmaticTalks.Controllers
{
    [Produces("application/json")]
    [Route("api/talks")]
    public class TalksApiController : Controller
    {
        private readonly PragmaticContext _context;

        public TalksApiController(PragmaticContext context)
        {
            _context = context;
        }

        // GET: api/TalksApi
        [HttpGet]
        public IEnumerable<Talk> GetTalks()
        {
            return _context.Talks;
        }

        // GET: api/TalksApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTalk([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var talk = await _context.Talks.SingleOrDefaultAsync(m => m.Id == id);

            if (talk == null)
            {
                return NotFound();
            }

            return Ok(talk);
        }

        // PUT: api/TalksApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTalk([FromRoute] int id, [FromBody] Talk talk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != talk.Id)
            {
                return BadRequest();
            }

            _context.Entry(talk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TalkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TalksApi
        [HttpPost]
        public async Task<IActionResult> PostTalk([FromBody] Talk talk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Talks.Add(talk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTalk", new { id = talk.Id }, talk);
        }

        // DELETE: api/TalksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTalk([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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