﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PragmaticTalks.Data;

namespace PragmaticTalks.Controllers
{
    [Produces("application/json")]
    [Route("api/speakers")]
    public class SpeakersApiController : Controller
    {
        private readonly PragmaticContext _context;

        public SpeakersApiController(PragmaticContext context)
        {
            _context = context;
        }

        // GET: api/SpeakersApi
        [HttpGet]
        public IEnumerable<Speaker> GetSpeakers()
        {
            return _context.Users;
        }

        // GET: api/SpeakersApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpeaker([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var speaker = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (speaker == null)
            {
                return NotFound();
            }

            return Ok(speaker);
        }

        // PUT: api/SpeakersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeaker([FromRoute] string id, [FromBody] Speaker speaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != speaker.Id)
            {
                return BadRequest();
            }

            _context.Entry(speaker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeakerExists(id))
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

        // POST: api/SpeakersApi
        [HttpPost]
        public async Task<IActionResult> PostSpeaker([FromBody] Speaker speaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(speaker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpeaker", new { id = speaker.Id }, speaker);
        }

        // DELETE: api/SpeakersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeaker([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var speaker = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            _context.Users.Remove(speaker);
            await _context.SaveChangesAsync();

            return Ok(speaker);
        }

        private bool SpeakerExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}