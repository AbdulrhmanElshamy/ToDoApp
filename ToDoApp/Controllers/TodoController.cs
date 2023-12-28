using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Modles;

namespace ToDoApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            var Items = await _dbContext.Items.ToListAsync();

            return Ok(Items);
        }

        [HttpPost]

        public async Task<IActionResult> Create(ItemData item)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("GetItem",new {item.Id},item); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = _dbContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ItemData item)
        {
            if(id != item.Id) return BadRequest();

            var existItem = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if(existItem == null) return NotFound();    

            existItem.Title = item.Title;
            existItem.Description = item.Description;
            existItem.Done = item.Done;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existItem = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (existItem == null) return NotFound();

             _dbContext.Items.Remove(existItem);
            await _dbContext.SaveChangesAsync();

            return Ok(existItem);
        }


    }
}
