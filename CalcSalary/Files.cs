using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace CalcSalary
{
    public class Files
    {
        public const string manFile = @"..\..\..\Files\managerHoursWorkedList.csv";     //List of all managers with worked hours
        public const string empFile = @"..\..\..\Files\employeeHoursWorkedList.csv";    //List of all employees with worked hours
        public const string freeFile = @"..\..\..\Files\freelancerHoursWorkedList.csv"; //List of all freelancers with worked hours
        public const string employeeListFile = @"..\..\..\Files\employeeListFile.csv";  //List of all employees

        public List<Person> Reader(List<string> path)
        {
            ActionsOfEmployees actionsOfEmployees = new ActionsOfEmployees();
            List<Person> data = new List<Person>();
            foreach (var itemPath in path)
            {
                using (TextFieldParser parser = new TextFieldParser(itemPath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        var allData = ClassIdentification(fields[1]);

                        allData.Date = DateTime.Parse(fields[0]);
                        allData.Name = fields[1];
                        allData.Hours = byte.Parse(fields[2]);
                        allData.Message = fields[3];

                        allData.Role = actionsOfEmployees.RoleIdentification(allData.Name);
                        allData.Pay = actionsOfEmployees.SalaryCalc(allData.Hours, allData.Role);
                        data.Add(allData);
                    }
                }
            }
            return data;
        }
        public void Writer(string path, Person data, bool newEmployee)
        {
            if (!newEmployee)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"{data.Date.ToShortDateString()}, {data.Name}, {data.Hours}, {data.Message}");
                }
            }
            else
            {
                using (StreamWriter employeeListStreamWriter = new StreamWriter(employeeListFile, true))
                {
                    employeeListStreamWriter.WriteLine($"{data.Name}, {data.Role}");
                }
            }
        }
        public string PathIdentification(string name)
        {
            string path = null;
            using (TextFieldParser parser = new TextFieldParser(employeeListFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields[0].ToLower() == name.ToLower())
                    {
                        if (fields[1].ToLower() == "Manager".ToLower())
                        {
                            path = manFile;
                        }
                        else if (fields[1].ToLower() == "Employee".ToLower())
                        {
                            path = empFile;
                        }
                        else if (fields[1].ToLower() == "Freelancer".ToLower())
                        {
                            path = freeFile;
                        }
                        break;
                    }
                }
            }
            return path;
        }

        public Person ClassIdentification(string name)
        {
            Manager m = new Manager(name);
            Employee e = new Employee(name);
            Freelancer f = new Freelancer(name);
            ActionsOfEmployees actionsOfEmployees = new ActionsOfEmployees();

            Settings.Role roleIdentification = actionsOfEmployees.RoleIdentification(name);

            if (roleIdentification == Settings.Role.Manager)
            { return m; }

            else if (roleIdentification == Settings.Role.Employee)
            { return e; }

            else
            { return f; }
        }

        public List<string> AllPathsIdentification()
        {
            List<string> listOfPaths = new List<string>();
            using (TextFieldParser parser = new TextFieldParser(employeeListFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields[1].ToLower() == "Manager".ToLower() && !listOfPaths.Contains(manFile))
                    {
                        listOfPaths.Add(manFile);
                    }
                    else if (fields[1].ToLower() == "Employee".ToLower() && !listOfPaths.Contains(empFile))
                    {
                        listOfPaths.Add(empFile);
                    }
                    else if (fields[1].ToLower() == "Freelancer".ToLower() && !listOfPaths.Contains(freeFile))
                    {
                        listOfPaths.Add(freeFile);
                    }
                }
            }
            return listOfPaths;         
        }
    }  
}
