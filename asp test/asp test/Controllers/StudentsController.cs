using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Data;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: api/students
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(StudentData.Students);
        }

        // GET: api/students/{id}
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = StudentData.GetStudent(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            student.Id = StudentData.Students.Max(s => s.Id) + 1;
            StudentData.Students.Add(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = StudentData.GetStudent(id);
            if (student == null) return NotFound();

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;

            return NoContent();
        }

        // DELETE: api/students/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = StudentData.GetStudent(id);
            if (student == null) return NotFound();

            StudentData.Students.Remove(student);
            return NoContent();
        }
    }
}
