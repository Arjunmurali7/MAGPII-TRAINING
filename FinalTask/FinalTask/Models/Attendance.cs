using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendanceAPI.Models
{
    public class Attendance
    {
        public int Id { get; set; }// primary key

        [Required]
        public string EmployeeName { get; set; } // employee name

        [Required]
        public string EmployeeId { get; set; }// employee id

        [Required]
        [DataType(DataType.Date)]// specify that this field is a date
        [CustomValidation(typeof(Attendance), nameof(ValidateDate))]// Custom validation  call the ValidateDate method
        public DateTime Date { get; set; }

        [Required]
        [EnumDataType(typeof(StatusEnum))]// Ensure the status is one of the defined enum values
        public string Status { get; set; }

        public static ValidationResult ValidateDate(DateTime date, ValidationContext context)// custom validation method to check if date is not in the future
        {
            if (date > DateTime.Now)// if date is in the future
                return new ValidationResult("Date cannot be in the future.");// return validation error if date is in the future
            return ValidationResult.Success;// return success if date is valid
        }
    }

    public enum StatusEnum// define the possible status values
    {
        Present,
        Absent,
        Leave
    }
}
