using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GenericController<T> : ControllerBase where T : class
{
    private readonly DbContext _dbContext;

    public GenericController(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/Generic
    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> Get()
    {
        var entities = await _dbContext.Set<T>().ToListAsync();
        return Ok(entities);
    }

    // GET: api/Generic/5
    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    // POST: api/Generic
    [HttpPost]
    public async Task<ActionResult<T>> Post(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("Get", new { id = GetEntityId(entity) }, entity);
    }

    // PUT: api/Generic/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, T entity)
    {
        if (id != GetEntityId(entity))
        {
            return BadRequest();
        }

        _dbContext.Entry(entity).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EntityExists(id))
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

    // DELETE: api/Generic/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool EntityExists(int id)
    {
        return _dbContext.Set<T>().Any(e => GetEntityId(e) == id);
    }

    private int GetEntityId(T entity)
    {
        var property = typeof(T).GetProperty("Id");
        if (property == null)
        {
            throw new InvalidOperationException("The entity must have a property named 'Id'.");
        }

        return (int)property.GetValue(entity);
    }
}
