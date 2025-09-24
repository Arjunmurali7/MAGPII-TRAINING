using System.ComponentModel.DataAnnotations;

namespace StudentRecord.Models
{
    public class Student
    {
        [Required]
        public string RollNumber { get; set; } //unique identifier

        public string Name { get; set; }//student name

        public int Marks { get; set; }//student marks
    }
} 