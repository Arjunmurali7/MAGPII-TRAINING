using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using System.Collections.Generic;

namespace StudentApi.Controllers
{
    public class StudentsController : Controller
    {
        // Dummy in-memory data
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "John", Age = 20, Course = "Computer Science" },
            new Student { Id = 2, Name = "Alice", Age = 22, Course = "Mathematics" },
            new Student { Id = 3, Name = "Bob", Age = 21, Course = "Physics" }
        };

        // GET: /Students
        public IActionResult Index()
        {
            return View(_students);
        }
    }
}
