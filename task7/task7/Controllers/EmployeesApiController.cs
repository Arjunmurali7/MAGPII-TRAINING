using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task7.Data;
using task7.Models;

[Route("api/[controller]")]
[ApiController]
public class EmployeesApiController : ControllerBase
{
    private readonly AppDbContext _db;
    public EmployeesApiController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _db.Employees.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Post(Employee emp)
    {
        _db.Employees.Add(emp);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Post), new { id = emp.Id }, emp);
    }
}
