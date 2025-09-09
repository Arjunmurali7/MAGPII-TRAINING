using StudentAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentAPI.Data
{
    public static class StudentData
    {
        public static List<Student> Students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Age = 20 },
            new Student { Id = 2, Name = "Bob", Age = 22 }
        };

        public static Student? GetStudent(int id) => Students.FirstOrDefault(s => s.Id == id);
    }
}
