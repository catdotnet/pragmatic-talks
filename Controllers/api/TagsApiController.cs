﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PragmaticTalks.Data;
using PragmaticTalks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PragmaticTalks.Controllers
{
    [Produces("application/json")]
    [Route("api/tags")]
    public class TagsApiController : ApiController
    {
        private readonly PragmaticContext _context;

        public TagsApiController(PragmaticContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Tag> GetTags()
        {
            return _context.Tags.OrderBy(t => t.Name);
        }

        [HttpGet("top")]
        public IEnumerable<TagTopListItem> GetTopTags(int number = 10)
        {
            var query = _context.TalkTags.GroupBy(tt => tt.Tag).Select(g => new TagTopListItem
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Color = g.Key.Color,
                TalksCount = g.Count()
            });

            query = query.OrderByDescending(t => t.TalksCount);

            if (number > 0)
                query = query.Take(number);

            var model = query.ToList();
            return model;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(int page = 0, int pageSize = 10, string orderBy = null, string search = null)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();

            Expression<Func<Tag, bool>> preCondition = t => true;

            Expression<Func<Tag, Tag>> selector = t => t;

            var model = await _context.Tags.FindOrderedPagedProjectionAsync(page, pageSize, preCondition, search, orderBy, "name", selector);

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag([FromRoute] int id, [FromBody] Tag tag)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != tag.Id) return BadRequest();

            tag.Name = tag.Name.RemoveDiacritics().ToLowerInvariant();
            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(@tag);
        }

        [HttpPost]
        public async Task<IActionResult> PostTag([FromBody] Tag tag)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            tag.Name = tag.Name.RemoveDiacritics().ToLowerInvariant();
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return Created(tag);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag([FromRoute] int id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var tag = await _context.Tags.SingleOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return Ok(tag);
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}