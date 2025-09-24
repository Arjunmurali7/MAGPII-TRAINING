using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StudentRecord.Helpers;
using StudentRecord.Models;
using TASK17.Models;

namespace StudentRecord.Controllers
{
    public class StudentController : Controller//inherits from controller class
    {
        private readonly IWebHostEnvironment _env;//to access web root path,content root path
        public StudentController(IWebHostEnvironment env)//constructor to initialize env
        {
            _env = env;//initialize keep instance for use
        }

        // Show all students
        public IActionResult Index() //reads all students from file and displays in view
        {
            var students = FileHelper.ReadAll(_env);//read all students from file
            return View(students);
        }


        public IActionResult Create() //to show create form to add new student
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student) //to handle form submission
        {
            if (!ModelState.IsValid) return View(student);

            //prevent duplicate roll number  
            var existing = FileHelper.FindByRoll(_env, student.RollNumber);
            if (existing != null)
            {
                ModelState.AddModelError("RollNumber", "A student with this roll number already exists.");
                return View(student);
            }

            FileHelper.AppendStudent(_env, student); //append new student to file
            return RedirectToAction(nameof(Index));
        }

        // Search form
        public IActionResult Search() //to show search form
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string rollNumber) //handle search form submission
        {
            if (string.IsNullOrWhiteSpace(rollNumber))
            {
                ModelState.AddModelError("", "Enter a roll number.");
                return View();
            }

            var student = FileHelper.FindByRoll(_env, rollNumber.Trim()); //search student by roll number
            if (student == null) return View("NotFound", rollNumber.Trim());//if not found show notfound view

            return View("Details", student);
        }

        // Details by roll
        public IActionResult Details(string id) //show details of student by roll number
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();
            var student = FileHelper.FindByRoll(_env, id); //search student by roll number
            if (student == null) return NotFound();
            return View(student);
        }
    }
}