using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XMLSerialization
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }

    [XmlRoot("Company")]
    public class Company
    {
        [XmlArray("Employees")]
        [XmlArrayItem("Employee")]
        public List<Employee> Employees { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Task 1: Deserialize XML
            Company company;
            using (var stream = new FileStream("samplexml.xml", FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(Company));
                company = (Company)serializer.Deserialize(stream);
            }

            // Add a new employee
            var newEmployee = new Employee
            {
                Id = 101,
                Name = "John Doe",
                Department = "HR"
            };
            company.Employees.Add(newEmployee);

            // Serialize the new model
            using (var stream = new FileStream("newxml.xml", FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(Company));
                serializer.Serialize(stream, company);
            }

            Console.WriteLine("XML serialization and deserialization completed successfully!");
        }
    }
}