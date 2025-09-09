using System;

namespace StudentApp
{
    class Student
    {

        public string Name { get; set; }
        public int Age { get; set; }


        public void PrintDetails()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student();


            Console.Write("Enter Student Name: ");
            student1.Name = Console.ReadLine();

            Console.Write("Enter Student Age: ");
            student1.Age = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("\nStudent Details:");
            student1.PrintDetails();

            Console.ReadLine();
        }
    }
}
