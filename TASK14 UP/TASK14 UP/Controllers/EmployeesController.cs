using Microsoft.AspNetCore.Mvc;
using SerializationMVC.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SerializationMVC.Controllers
{
    public class EmployeesController : Controller//Controller base class provides methods and properties for handling HTTP requests and generating responses.
    {
        private readonly string dataFolder; //stores full path to the App_Data.
        private readonly string xmlFilePath; //stores the full path to the XML file 

        public EmployeesController() //Constructor initializes the dataFolder and xmlFilePath fields
        {
            dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");  ////Combines the current project root folder with App_Data 
            if (!Directory.Exists(dataFolder)) Directory.CreateDirectory(dataFolder); //Creates the App_Data folder if it doesn't exist
            xmlFilePath = Path.Combine(dataFolder, "employees.xml"); //Combines the dataFolder path with employees.xml to get the full path to the XML file
        }

        public IActionResult Index() //Displays the list of employees by deserializing the XML file if it exists
        {
            var employees = new List<Employee>(); //if xml file exist then the list will be shown

            if (System.IO.File.Exists(xmlFilePath)) //Check if the XML file exists
            {
                using (var fs = new FileStream(xmlFilePath, FileMode.Open)) //Open the file in read mode
                {
                    var serializer = new XmlSerializer(typeof(List<Employee>)); //Creates an instance of the XmlSerializer class.
                    employees = (List<Employee>)serializer.Deserialize(fs); //Deserialize the XML content back into a list of Employee objects
                }
            }

            return View(employees);
        }

        [HttpPost]
        public IActionResult CreateAndSerialize() //Creates a list of Employee objects and serializes them to an XML file
        {
            var employees = new List<Employee>
            {
                new Employee{ Id=1, Name="Arjun", Department="HR", Salary=50000 },
                new Employee{ Id=2, Name="isak", Department="IT", Salary=60000 },
                new Employee{ Id=3, Name="neymar", Department="Finance", Salary=55000 },
                new Employee{ Id=4, Name="messi", Department="IT", Salary=40000 },
                new Employee{ Id=5, Name="ronaldo", Department="IT", Salary=35000 },
            };

            using (var fs = new FileStream(xmlFilePath, FileMode.Create))  // if file exist it be overwritten if not then create
            {
                var serializer = new XmlSerializer(typeof(List<Employee>));  //Creates an instance of the XmlSerializer class.
                serializer.Serialize(fs, employees);//Serialize the list of Employee objects to the XML file
            }

            TempData["Message"] = "Employees serialized to employees.xml successfully!";
            return View("Index", new List<Employee>()); // empty list
        }

        [HttpPost]
        public IActionResult DeserializeEmployees() //Reads the XML file and deserializes its content back into a list of Employee objects
        {
            var employees = new List<Employee>(); //if xml file exist then the list will be shown

            if (System.IO.File.Exists(xmlFilePath)) //Check if the XML file exists
            {
                using (var fs = new FileStream(xmlFilePath, FileMode.Open)) 
                {
                    var serializer = new XmlSerializer(typeof(List<Employee>));
                    employees = (List<Employee>)serializer.Deserialize(fs); //Deserialize the XML content back into a list of Employee objects
                }

                TempData["Message"] = "Employees deserialized from XML successfully!";
            }
            else
            {
                TempData["Message"] = "No XML file found. Please serialize first.";
            }

            return View("Index", employees);
        }
    }
}