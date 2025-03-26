using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    public class EventsController : Controller
    {
        readonly ContextDb _context;
        public EventsController(ContextDb context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/events/all")]
        public async Task<IActionResult> GetAll(string? name, string? type, int? page, int? pageSize)
        {
            IQueryable<Event> query = _context.Events;

            if (!string.IsNullOrEmpty(name))
            {
                var nameParts = name.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(i => nameParts.Any(part => i.name.ToLower().Contains(part)));
            }
            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(x => x.type == type);

            }
            if (page.HasValue && pageSize.HasValue)
            {
                return Ok(query.Skip((int)(page - 1) * (int)pageSize).Take((int)pageSize).ToList());
            }
            return Ok(query.ToList());
        }
        [HttpPost]
        [Route("/api/events/add")]
        public async Task<IActionResult> AddNew([FromBody] EventRequest created)
        {

            var newevent = new Event()
            {
                name = created.name,
                type = created.type,
                date = created.date,
                city = created.city,
            };
            await _context.Events.AddAsync(newevent);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("/api/events/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EventRequest created)
        {
            var check = _context.Events.FirstOrDefault(x => x.id == id);
            check.name = created.name;
            check.type = created.type;
            check.date = created.date;
            check.city = created.city;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("/api/events/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var check = _context.Events.FirstOrDefault(x => x.id == id);
            _context.Events.Remove(check);
        await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("/api/events/event/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(

                             _context.Events.FirstOrDefault(x => x.id == id));


        }
    }
}
