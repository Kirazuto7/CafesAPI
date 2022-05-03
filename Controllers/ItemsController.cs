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
    public class ItemsController : ControllerBase
    {
        private readonly CafesAPIDBContext _context;

        public ItemsController(CafesAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<Response>> GetItem()
        {
            var items = await _context.Item.ToListAsync();

            var response = new Response();
            response.statusCode = 404;
            response.statusDescription = "No Items Found!";

            if(items.Any())
            {
                response.statusCode = 200;
                response.statusDescription = "Items Found.";
                response.items = items;
            }
            return response;
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetItem(int id)
        {
            var item = await _context.Item.FindAsync(id);

            var response = new Response();
            response.statusCode = 404;
            response.statusDescription = "Item Not Found!";
           
            if (item != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Found Item: " + item.ItemName;
                response.items.Add(item);
            }

            return response;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutItem(int id, Item item)
        {
            var response = new Response();

            if (id != item.ItemId)
            {
                response.statusCode = 400;
                response.statusDescription = "Bad Request!";
                return response;
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    response.statusCode = 404;
                    response.statusDescription = "Item Not Found!";
                    return response;
                }
                else
                {
                    throw;
                }
            }

            response.statusCode = 204;
            response.statusDescription = "Successfully Updated Item.";
            return response;
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostItem(Item item)
        {
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            var response = new Response();
            response.statusCode = 201;
            response.statusDescription = "Successfully Created Item with id: " + item.ItemId;
            response.items.Add(item);
            return response;
            //return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteItem(int id)
        {
            var item = await _context.Item.FindAsync(id);
            var response = new Response();
            
            if (item == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Item Not Found!";
                return response;
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            response.statusCode = 204;
            response.statusDescription = "Successfully Deleted Item.";
            return response;
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
