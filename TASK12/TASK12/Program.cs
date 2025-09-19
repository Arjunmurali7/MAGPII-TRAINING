using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
class Program
{
    static void Main()
    {
        string xmlFile = "Employees.xml"; //xml data
        string xsdFile = "Employees.xsd"; //schema rules



        //  Step1: Validate XML against XSD
        XmlSchemaSet schema = new XmlSchemaSet();
        schema.Add("", xsdFile);

        XmlDocument doc = new XmlDocument(); // create xml document
        doc.Schemas.Add(schema);  //add rules
        doc.Load(xmlFile);  //load data


        try
        {
            doc.Validate(null); // check if rules are followed
            Console.WriteLine("XML is valid against XSD.\n");
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.WriteLine("Validation Error: " + ex.Message);
            return;
        }

        // step2  xpath queries

        XPathNavigator nav = doc.CreateNavigator(); //nav= navigate

        Console.WriteLine("Employees in IT Department:");
        XPathNodeIterator itDept = nav.Select("/Employees/Employee[Department='IT']/Name");
        while (itDept.MoveNext())
            Console.WriteLine(" - " + itDept.Current.Value);

        Console.WriteLine("\nEmployees with Salary > 50000:");
        XPathNodeIterator itSalary = nav.Select("/Employees/Employee[Salary>50000]/Name");
        while (itSalary.MoveNext())
            Console.WriteLine(" - " + itSalary.Current.Value);

        Console.WriteLine("\nEmployees who joined after 01-01-2020:");
        DateTime cutoff = new DateTime(2020, 1, 1);
        foreach (XPathNavigator emp in nav.Select("//Employee"))
        {
            string dateStr = emp.SelectSingleNode("JoiningDate")?.Value;
            if (DateTime.TryParse(dateStr, out DateTime joinDate) && joinDate > cutoff)
            {
                Console.WriteLine(emp.SelectSingleNode("Name").Value);
            }
        }
    }
}





