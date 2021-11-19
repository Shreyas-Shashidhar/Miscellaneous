using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoService _toDoDataService;
        public ToDoItemsController(IToDoService toDoDataService)
        {
            _toDoDataService = toDoDataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok( _toDoDataService?.GetAll());
        }

        // GET api/<ToDoItemsController>/5
        [HttpGet("{id}")]
        public  IActionResult Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var item = _toDoDataService?.GetById(id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        // POST api/<ToDoItemsController>
        [HttpPost]
        public IActionResult Post([FromBody] ToDoItem value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = value.Id }, _toDoDataService?.Create(value));
        }

        // PUT api/<ToDoItemsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ToDoItem value)
        {
            if (string.IsNullOrWhiteSpace(id) || value == null)
            {
                return BadRequest();
            }

            var retStatus = _toDoDataService?.Update(id, value);
            if (retStatus.Value)
                return Ok(value);
            else
                return NotFound();
        }

        // DELETE api/<ToDoItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var retStatus =  _toDoDataService?.Delete(id);
            if (retStatus.Value)
                return NoContent();
            else
                return NotFound();
        }
    }
}
