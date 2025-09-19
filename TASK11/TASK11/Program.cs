using System;


namespace EmployeeCollections
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            int choice;

            do
            {
                Console.WriteLine("\nEmployee Management");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. View Employees");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Remove Employee");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // *Add Employee*
                        Console.Write("Enter Employee ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Employee Name: ");
                        string name = Console.ReadLine();

                        employees.Add(new Employee { Id = id, Name = name });
                        Console.WriteLine("Employee added successfully!");
                        break;

                    case 2:
                        // *View Employees*
                        Console.WriteLine("\nEmployee List:");
                        var sorted = employees.OrderBy(e => e.Name);

                        foreach (var emp in sorted)
                        {

                            Console.WriteLine(emp);
                        }
                        break;

                    case 3:
                        // *Update Employee*
                        Console.Write("Enter Employee ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Employee empToUpdate = employees.Find(e => e.Id == updateId);

                        if (empToUpdate != null)
                        {
                            Console.Write("Enter new name: ");
                            empToUpdate.Name = Console.ReadLine();
                            Console.WriteLine("Employee updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Employee not found!");
                        }
                        break;

                    case 4:
                        //*Remove Employee*
                        Console.Write("Enter Employee ID to remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        Employee empToRemove = employees.Find(e => e.Id == removeId);

                        if (empToRemove != null)
                        {
                            employees.Remove(empToRemove);
                            Console.WriteLine("Employee removed successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Employee not found!");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Try again.");
                        break;
                }

            } while (choice != 5);
        }
    }
}