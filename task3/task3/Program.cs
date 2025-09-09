using System;
using System.Collections.Generic;
using System.Linq;   

namespace StudentApp
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            List<Student> students = new List<Student>();

            try
            {
                Console.Write("How many students do you want to add? ");
                int count = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < count; i++)
                {
                    Console.Write("Enter Student Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Student Age: ");
                    int age = Convert.ToInt32(Console.ReadLine());

                    students.Add(new Student { Name = name, Age = age });
                }

                
                var filteredStudents = students.Where(s => s.Age >= 18)
                                               .OrderBy(s => s.Name)
                                               .Select(s => new { s.Name, s.Age });

                Console.WriteLine("\nStudents aged 18 and above:");
                foreach (var student in filteredStudents)
                {
                    Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("❌ Invalid input! Please enter a number for age.");
                Console.WriteLine($"Error details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Something went wrong.");
                Console.WriteLine($"Error details: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}

