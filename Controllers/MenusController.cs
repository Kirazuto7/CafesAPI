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
    public class MenusController : ControllerBase
    {
        private readonly CafesAPIDBContext _context;

        public MenusController(CafesAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<Response>> GetMenu()
        {
            var menus = await _context.Menu.Include(m => m.Items.OrderBy(i => i.ItemName).ThenBy(i => i.Price)).ToListAsync();
            var response = new Response();

            response.statusCode = 404;
            response.statusDescription = "Menus Not Found!";
            if(menus.Any())
            {
                response.statusCode = 200;
                response.statusDescription = "Menus Found!";
                response.menus = menus;
            }

            return response;
        }

        // GET: api/Menus/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Response>> GetMenu(int id)
        {
            var menu = await _context.Menu.Include(m => m.Items.OrderBy(i => i.ItemName).ThenBy(i => i.Price))
                                          .SingleOrDefaultAsync(i => i.MenuId == id);
            var response = new Response();
            response.statusCode = 404;
            response.statusDescription = "Menu Not Found!";
            if (menu != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Menu Found!";
                response.menus.Add(menu);
            }

            return response;
        }

        // GET: api/Menus/Americano
        [Route("api/menus/{itemName}")]
        [HttpGet("{itemName}")]
        public async Task<ActionResult<Response>> GetMenuItemByName(string itemName)
        {
            var menuItems = await _context.Menu.Include(m => m.Items.Where(i => i.ItemName == itemName)).ToListAsync();

            var response = new Response();
            response.statusCode = 404;
            response.statusDescription = "Menu with " + itemName + " Not Found!";
                
            for(int i = 0; menuItems.Count > i; i++)
            {
                if(menuItems[i].Items.Count > 0)
                {
                    response.statusCode = 200;
                    response.statusDescription = "Menus with " + itemName + " Found.";
                    response.menus.Add(menuItems[i]);
                }
            }

            return response;
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutMenu(int id, Menu menu)
        {
            var response = new Response();

            if (id != menu.MenuId)
            {
                response.statusCode = 400;
                response.statusDescription = "Bad Request!";
                return response;
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    response.statusCode = 404;
                    response.statusDescription = "Menu Not Found!";
                    return response;
                }
                else
                {
                    throw;
                }
            }

            response.statusCode = 204;
            response.statusDescription = "Successfully Updated Menu.";
            return response;
        }

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostMenu(Menu menu)
        {
            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            var response = new Response();
            response.statusCode = 201;
            response.statusDescription = "Successfully Created Menu with id: " + menu.MenuId;
            response.menus.Add(menu);
            return response;

            //return CreatedAtAction("GetMenu", new { id = menu.MenuId }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);

            var response = new Response();

            if (menu == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Menu Not Found!";
                return response;
            }

            if (menu.Items.Any())
            {
                foreach(Item item in menu.Items)
                {
                    _context.Item.Remove(item);
                }
            }

            _context.Menu.Remove(menu);

            await _context.SaveChangesAsync();

            response.statusCode = 204;
            response.statusDescription = "Successfully Deleted Menu.";
            return response;
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.MenuId == id);
        }
    }
}
