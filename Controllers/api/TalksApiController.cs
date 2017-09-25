using Microsoft.AspNetCore.Mvc;
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
    [Route("api/talks")]
    public class TalksApiController : ApiController
    {
        private readonly PragmaticContext _context;

        public TalksApiController(PragmaticContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<TalkListItem> GetAsync()
        {
            return _context.Talks.Where(t => t.IsDeleted == false && t.EventId == null).OrderBy(t => t.SpeakerTalkCount).ThenBy(t => t.DateCreation).Select(t => new TalkListItem
            {
                Title = t.Title,
                IsSelected = t.IsSelected,
                Tags = t.Tags.Select(l => new TagItem { Name = l.Tag.Name, Color = l.Tag.Color }).ToArray(),
                Language = t.Language
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(int page = 0, int pageSize = 10, string orderBy = null, string search = null, bool onlyOpened = true)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();

            Expression<Func<Talk, bool>> preCondition;
            if (onlyOpened) preCondition = t => t.IsDeleted == false && t.EventId == null;
            else preCondition = p => true;

            Expression<Func<Talk, TalkSearchItem>> selector = t => new TalkSearchItem
            {
                Id = t.Id,
                DateCreation =  t.DateCreation,
                IsAssignedToEvent = t.EventId != null,
                IsDeleted = t.IsDeleted,
                IsSelected = t.IsSelected,
                SpeakerName = t.Speaker.DisplayName,
                Title = t.Title
            };

            var model = await _context.Talks.FindOrderedPagedProjectionAsync(page, pageSize, preCondition, search, orderBy, "title", selector);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TalkCreation request)
        {
            if (CurrentUser == null) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var requestedTags = request.Tags.Select(s => s.RemoveDiacritics().ToLowerInvariant()).ToArray();
            var tags = await _context.Tags.Where(t => requestedTags.Contains(t.Name)).ToListAsync();
            var newTags = requestedTags.Where(t => !tags.Any(i => i.Name == t)).Select(t => new Tag { Name = t });
            tags.AddRange(newTags);

            if (tags.Count > 3) return BadRequest(ModelState);

            var talk = new Talk
            {
                Title = request.Title,
                Language = request.Language,
                DateCreation = DateTime.UtcNow,
                Speaker = CurrentUser
            };
            talk.Tags = tags.Select(t => new TalkTag { Tag = t, Talk = talk }).ToList();

            _context.Talks.Add(talk);

            CurrentUser.TalksCounter += 1;
            _context.Entry(CurrentUser).State = EntityState.Modified;

            talk.SpeakerTalkCount = CurrentUser.TalksCounter;

            await _context.SaveChangesAsync();

            return Created(new { id = talk.Id });
        }

        [HttpPatch("{id}/selected")]
        public async Task<IActionResult> MarkAsSelectedAsync([FromRoute] int id)
        {
            var selectedCount = await _context.Talks.Include("Speaker").CountAsync(t => t.IsSelected && t.EventId == null);
            if (selectedCount >= 5) return BadRequest("There are already 5 talks selected");

            return await ModifyTalkAsync(id, talk => talk.IsSelected = true);
        }

        [HttpPatch("{id}/unselected")]
        public Task<IActionResult> MarkAsUnselectedAsync([FromRoute] int id)
        {
            return ModifyTalkAsync(id, talk => talk.IsSelected = false);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (CurrentUser == null) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var talk = await _context.Talks.SingleOrDefaultAsync(m => m.Id == id);
            if (talk == null) return NotFound();
            if (talk.Speaker.Email != CurrentUser.Email && !CurrentUser.IsAdministrator) return Forbidden();

            talk.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(talk);
        }

        private async Task<IActionResult> ModifyTalkAsync(int id, Action<Talk> action)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var talk = await _context.Talks.Include("Speaker").SingleOrDefaultAsync(m => m.Id == id);
            if (talk == null) return NotFound();

            action(talk);

            await _context.SaveChangesAsync();

            return Ok(new TalkSearchItem
            {
                Id = talk.Id,
                DateCreation = talk.DateCreation,
                IsAssignedToEvent = talk.EventId != null,
                IsDeleted = talk.IsDeleted,
                IsSelected = talk.IsSelected,
                SpeakerName = talk.Speaker.DisplayName,
                Title = talk.Title
            });
        }
    }
}