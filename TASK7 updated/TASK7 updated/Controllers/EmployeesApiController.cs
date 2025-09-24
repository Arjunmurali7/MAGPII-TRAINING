using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeManagement.Controllers // api controller
{
    [Route("api/[controller]")] // route remove controller from url cntrlclass name
    [ApiController]// attribute to indicate it's an API controller
    public class EmployeesApiController : ControllerBase // inhert from controllerbase
    {
        private readonly AppDbContext _context; // field efcore/ dbcontext acess database

        public EmployeesApiController(AppDbContext context) // constructer with It takes a parameter of type AppDbContext
        {
            _context = context; // parameter to field 
        }

        [HttpGet]
        public IActionResult GetEmployees()// get all emp
        {
            return Ok(_context.Employees.ToList()); //covert emp row to list and return ok response
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)/// get emp by id
        {
            var emp = _context.Employees.Find(id); // find emp by id
            if (emp == null) return NotFound(); // if not found return 404
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee employee) // create emp
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee); // return 201 with location header
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee employee)// update emp
        {
            if (id != employee.Id) return BadRequest(); // id in url and body must match
            if (!_context.Employees.Any(e => e.Id == id)) return NotFound(); // check emp exists

            _context.Employees.Update(employee);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)// delete emp
        {
            var emp = _context.Employees.Find(id); // find emp by id
            if (emp == null) return NotFound();// if not found return 404

            _context.Employees.Remove(emp); // remove emp
            _context.SaveChanges();// save changes
            return NoContent();
        }
    }
}
