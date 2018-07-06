using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Shared;

namespace TodoApp.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/ToDo")]
    public class ToDoListController : Controller
    {
        private readonly ToDoListContext _context;

        public ToDoListController(ToDoListContext context)
        {
            _context = context;
        }

        // GET: api/ToDo
        [HttpGet]
        public IEnumerable<ToDoList> GetToDo()
        {
            return _context.ToDoLists;
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<IActionResult> PostToDo([FromBody] ToDoList todo)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.ToDoLists.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetToDo" , new { id = todo.Id } , todo);
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo([FromRoute] int id , [FromBody] ToDoList todo)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(id != todo.Id)
                return BadRequest();

            _context.Entry(todo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!ToDoExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        {
            var ToDo = await _context.ToDoLists.SingleOrDefaultAsync(m => m.Id == id);
            if(ToDo == null)
                return NotFound();

            _context.ToDoLists.Remove(ToDo);
            await _context.SaveChangesAsync();
            return Ok(ToDo);
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDoLists.Any(e => e.Id == id);
        }
    }
}