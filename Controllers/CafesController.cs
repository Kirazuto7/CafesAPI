#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafesAPI.Models;

namespace CafesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafesController : ControllerBase
    {
        private readonly CafesAPIDBContext _context;

        public CafesController(CafesAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Cafes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cafe>>> GetCafes()
        {
            var cafes = await _context.Cafe.Include(c => c.Location)
                                           .Include(c => c.Menu).ThenInclude(c => c.Items.OrderBy(c => c.ItemName).ThenBy(c=> c.Price))
                                           .Include(c => c.Schedule)
                                           .ToListAsync();
            
            return cafes;
        }

        // GET: api/Cafes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetCafe(int id)
        {
            var cafe = await _context.Cafe.Include(c => c.Location)
                                          .Include(c => c.Menu).ThenInclude(c => c.Items.OrderBy(c => c.ItemName).ThenBy(c => c.Price))
                                          .Include(c => c.Schedule)
                                          .SingleOrDefaultAsync(i => i.CafeId == id);
            
            var response = new Response();

        
            response.statusCode = 404;
            response.statusDescription = "Not Found";

            if (cafe != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Ok!";
                response.cafes.Add(cafe);
            }

            return response;
        }

        // GET: api/Cafes/5/items/Americano
        [HttpGet("{id}/items/{itemName}")]
        public async Task<ActionResult<Response>> GetCafeItem(int id, string itemName)
        {
            var cafeItem = await _context.Cafe.Include(c => c.Menu)
                                           .ThenInclude(c => c.Items.Where(i => i.ItemName == itemName))
                                           .SingleOrDefaultAsync(i => i.CafeId == id);
     
            var response = new Response();
            response.statusCode = 404;
            response.statusDescription = "Not Found";

            if (cafeItem != null && cafeItem.Menu.Items.Count != 0 )
            {
                response.statusCode = 200;
                response.statusDescription = "Ok!";
                response.cafes.Add(cafeItem);
            }

            return response;
        }

        // GET: api/Cafes/id/Starbucks
        [HttpGet("id/{cafeName}")]
        public async Task<ActionResult<IEnumerable<Cafe>>> GetCafesByName(string cafeName)
        {
            var cafes = await _context.Cafe.Include(c => c.Location)
                                           .Include(c => c.Menu).ThenInclude(c => c.Items.OrderBy(c => c.ItemName).ThenBy(c => c.Price))
                                           .Include(c => c.Schedule)
                                           .Where(c => c.CafeName == cafeName)
                                           .ToListAsync();

            return cafes;
        }

        // PUT: api/Cafes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCafe(int id, Cafe cafe)
        {
            if (id != cafe.CafeId)
            {
                return BadRequest();
            }

            _context.Entry(cafe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CafeExists(id))
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

        // POST: api/Cafes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cafe>> PostCafe(Cafe cafe)
        {
            _context.Cafe.Add(cafe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCafe", new { id = cafe.CafeId }, cafe);
        }

        // DELETE: api/Cafes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafe(int id)
        {
            var cafe = await _context.Cafe.Include(c => c.Location)
                                          .Include(c => c.Menu).ThenInclude(c => c.Items.OrderBy(c => c.ItemName).ThenBy(c => c.Price))
                                          .Include(c => c.Schedule).FirstOrDefaultAsync(c => c.CafeId == id);

            if (cafe == null)
            {
                return NotFound();
            }

            _context.Cafe.Remove(cafe);

            if(cafe.Location != null)
            {
                var location = await _context.Location.FirstOrDefaultAsync(l => l.CafeId == id);
                _context.Location.Remove(location);
            }

            if (cafe.Menu != null)
            {
                var menu = await _context.Menu.FirstOrDefaultAsync(l => l.CafeId == id);
                _context.Menu.Remove(menu);
                
                if(menu.Items.Count > 0) // Items exist in the menu array
                {
                    foreach(Item item in menu.Items)
                    {
                        _context.Item.Remove(item);
                    }
                }
            }

            if (cafe.Schedule != null)
            {
                var schedule = await _context.Schedule.FirstOrDefaultAsync(l => l.CafeId == id);
                _context.Schedule.Remove(schedule);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CafeExists(int id)
        {
            return _context.Cafe.Any(e => e.CafeId == id);
        }
    }
}
