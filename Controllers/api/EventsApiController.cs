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
    [Route("api/events")]
    public class EventsController : ApiController
    {
        private readonly PragmaticContext _context;

        public EventsController(PragmaticContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet("next")]
        public async Task<IActionResult> GetNextEventAsync()
        {
            var @event = await _context.Events.Include("Talks.Tags.Tag").Include("Talks.Speaker").Where(e => e.Date >= DateTime.Now.Date).OrderBy(e => e.Date).FirstOrDefaultAsync();
            if (@event == null) return NoContent();

            return Ok(new EventDetail
            {
                Id = @event.Id,
                Date = @event.Date,
                Name = @event.Name,
                Talks = @event.Talks.Select(t => t.Title),
                Tags = @event.Talks.SelectMany(t => t.Tags).Select(tt => tt.Tag).Distinct().Select(t => new TagItem { Name = t.Name, Color = t.Color }),
                Speakers = @event.Talks.Select(t => t.Speaker).Distinct().Select(s => new SpeakerItem { Name = s.DisplayName, AvatarUrl = s.AvatarUrl })
            });
        }

        [HttpGet("last")]
        public async Task<IActionResult> GetLastEventsAsync()
        {
            var events = await _context.Events.Include("Talks.Tags.Tag").Include("Talks.Speaker").Where(e => e.Date < DateTime.Now.Date).OrderByDescending(e => e.Date).Take(9).ToListAsync();
            var model = events.Select(@event => new EventDetail
            {
                Id = @event.Id,
                Date = @event.Date,
                Name = @event.Name,
                Talks = @event.Talks.Select(t => t.Title),
                Tags = @event.Talks.SelectMany(t => t.Tags).Select(tt => tt.Tag).Distinct().Select(t => new TagItem { Name = t.Name, Color = t.Color }),
                Speakers = @event.Talks.Select(t => t.Speaker).Distinct().Select(s => new SpeakerItem { Name = s.DisplayName, AvatarUrl = s.AvatarUrl })
            });
            return Ok(model);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(int page = 0, int pageSize = 10, string orderBy = null, string search = null)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();

            Expression<Func<Event, bool>> preCondition = e => true;

            Expression<Func<Event, EventSearchItem>> selector = e => new EventSearchItem
            {
                Id = e.Id,
                Name = e.Name,
                Date = e.Date,
                Url = e.Url,
                Talks = e.Talks.Select(t => t.Title)
            };

            var model = await _context.Events.FindOrderedPagedProjectionAsync(page, pageSize, preCondition, search, orderBy, "name", selector);

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] EventModification request)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != request.Id) return BadRequest();

            var @event = await _context.Events.Include("Talks").SingleOrDefaultAsync(e => e.Id == id);
            if (@event == null) return NotFound();

            @event.Name = request.Name;
            @event.Date = request.Date;
            @event.Url = request.Url;

            await _context.SaveChangesAsync();

            return Ok(new EventSearchItem
            {
                Id = @event.Id,
                Name = @event.Name,
                Date = @event.Date,
                Url = @event.Url,
                Talks = @event.Talks.Select(t => t.Title)
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] Event @event)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var selectedTalks = await _context.Talks.Where(t => t.IsSelected && t.EventId == null).ToListAsync();
            if (selectedTalks.Count != 5) return BadRequest("There are not 5 selected talks yet");

            @event.Talks = selectedTalks;

            _context.Events.Add(@event);

            await _context.SaveChangesAsync();

            return Created(new EventSearchItem
            {
                Id = @event.Id,
                Name = @event.Name,
                Date = @event.Date,
                Url = @event.Url,
                Talks = @event.Talks.Select(t => t.Title)
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator) return Forbidden();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var @event = await _context.Events.Include("Talks").SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null) return NotFound();

            foreach (var talk in @event.Talks)
            {
                talk.EventId = null;
                talk.Event = null;
                talk.IsSelected = false;
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(new EventSearchItem
            {
                Id = @event.Id,
                Name = @event.Name,
                Date = @event.Date,
                Url = @event.Url,
                Talks = new string[0]
            });
        }
    }
}