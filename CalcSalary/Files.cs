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
        public const string manFile = @"..\..\..\Files\managerHoursWorkedList.csv";     //список всех менеджеров с отработанными часами
        public const string empFile = @"..\..\..\Files\employeeHoursWorkedList.csv";    //список всех сотрудников на зарплате с отработанными часами
        public const string freeFile = @"..\..\..\Files\freelancerHoursWorkedList.csv"; //список всех фрилансеров с отработанными часами
        public const string employeeListFile = @"..\..\..\Files\employeeListFile.csv";  //список всех сотрудников с указанием ролей

        public List<AllCurrentData> Reader(List<string> path)
        {
            ActionsOfEmployees a = new ActionsOfEmployees();
            List <AllCurrentData> data = new List<AllCurrentData>();
            foreach (var itemPath in path)
            {
                using (TextFieldParser parser = new TextFieldParser(itemPath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        AllCurrentData allData = new AllCurrentData();
                        allData.date = DateTime.Parse(fields[0]);
                        allData.name = fields[1];
                        allData.hours = byte.Parse(fields[2]);
                        allData.message = fields[3];
                    
                        allData.role = a.RoleIdentification(allData.name);
                        allData.pay = a.SalaryCalc(allData.hours, allData.role);
                        data.Add(allData);
                    }
                }
            }            
            return data;
        }
        public void Writer(string path, AllCurrentData data, bool newEmployee)
        {
            if (!newEmployee)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"{data.date.ToShortDateString()}, {data.name}, {data.hours}, {data.message}");
                }
            }
            else
            {
                using (StreamWriter employeeListStreamWriter = new StreamWriter(employeeListFile, true))
                {
                    employeeListStreamWriter.WriteLine($"{data.name}, {data.role}");
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
