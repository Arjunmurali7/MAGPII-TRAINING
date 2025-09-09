using System.ComponentModel.DataAnnotations;

namespace task7.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Department { get; set; }
    }
}
