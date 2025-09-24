using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller// mvc controller inhert from controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context) // constructor with param di
        {
            _context = context; // store the injected context in a private field di
        }

        public IActionResult Index() // fetch emp give to index
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        public IActionResult Create() => View(); //create view

        [HttpPost]
        public IActionResult Create(Employee employee) ///adds emp to db
        {
            if (ModelState.IsValid)// validate
            {
                _context.Employees.Add(employee);// add to db
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Edit(int id)///emp id edit
        {
            var emp = _context.Employees.Find(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee) // update db
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Delete(int id) // delete confrommation
        {
            var emp = _context.Employees.Find(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)///remove from db
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
