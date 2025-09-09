using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task7.Data;
using task7.Models;

namespace task7.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _db;
        public EmployeesController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Employees.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _db.Add(emp);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }
    }
}
