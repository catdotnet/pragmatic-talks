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
    [Route("api/speakers")]
    public class SpeakersApiController : ApiController
    {
        private readonly PragmaticContext _context;

        public SpeakersApiController(PragmaticContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet("top")]
        public IEnumerable<SpeakerTopListItem> GetSpeakers(int number = 10)
        {
            var query = _context.Users.Where(u => u.TalksCounter > 0).Select(s => new SpeakerTopListItem
            {
                Id = s.Id,
                DisplayName = s.DisplayName,
                AvatarUrl = s.AvatarUrl,
                TalksCounter = s.Talks.Count(t => !t.IsDeleted),
                TalksInEventsCounter = s.Talks.Count(t => !t.IsDeleted && t.EventId != null)
            });

            query = query.OrderByDescending(u => u.TalksInEventsCounter).ThenByDescending(u => u.TalksInEventsCounter).ThenBy(u => u.DisplayName);

            if (number > 0)
                query = query.Take(number);

            return query.ToList();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(int page = 0, int pageSize = 10, string orderBy = null, string search = null)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();

            Expression<Func<Speaker, bool>> preCondition = s => true;

            Expression<Func<Speaker, SpeakerSearchItem>> selector = s => new SpeakerSearchItem
            {
                Id = s.Id,
                DisplayName = s.DisplayName,
                Email = s.Email,
                AvatarUrl = s.AvatarUrl,
                IsAdministrator = s.IsAdministrator,
                IsBlocked = s.IsBlocked,
                TalksCounter = s.TalksCounter
            };

            var model = await _context.Users.FindOrderedPagedProjectionAsync(page, pageSize, preCondition, search, orderBy, "displayName", selector);

            return Ok(model);
        }

        [HttpPatch("{id}/makeadmin")]
        public Task<IActionResult> MakeAdminSpeakerAsync([FromRoute] string id)
        {
            return ModifySpeakerAsync(id, s => s.IsAdministrator = true);
        }

        [HttpPatch("{id}/unmakeadmin")]
        public Task<IActionResult> UnMakeAdminSpeakerAsync([FromRoute] string id)
        {
            return ModifySpeakerAsync(id, s => s.IsAdministrator = false);
        }

        [HttpPatch("{id}/block")]
        public Task<IActionResult> BlockSpeakerAsync([FromRoute] string id)
        {
            return ModifySpeakerAsync(id, s => s.IsBlocked = true);
        }

        [HttpPatch("{id}/unblock")]
        public Task<IActionResult> UnBlockSpeakerAsync([FromRoute] string id)
        {
            return ModifySpeakerAsync(id, s => s.IsBlocked = false);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeakerAsync([FromRoute] string id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var speaker = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (speaker == null) return NotFound();

            _context.Users.Remove(speaker);
            await _context.SaveChangesAsync();

            return Ok(speaker);
        }

        private async Task<IActionResult> ModifySpeakerAsync(string id, Action<Speaker> action)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var speaker = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (speaker == null) return NotFound();

            action(speaker);

            await _context.SaveChangesAsync();

            return Ok(new SpeakerSearchItem
            {
                Id = speaker.Id,
                DisplayName = speaker.DisplayName,
                Email = speaker.Email,
                AvatarUrl = speaker.AvatarUrl,
                IsAdministrator = speaker.IsAdministrator,
                IsBlocked = speaker.IsBlocked,
                TalksCounter = speaker.TalksCounter
            });
        }
    }
}