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

        public List<(DateTime dT, string name, byte hours, decimal pay, string message)> FilesAction(params string[] path)
        {
            ActionsOfEmployees a = new ActionsOfEmployees();
            byte hours = 0;
            decimal totalPay = 0;
            string name = "";
            string message = "";
            Settings.Role role;
            DateTime? date = null;
            List <(DateTime dT, string name, byte hours, decimal tPay, string message)> tupleData = new List<(DateTime dT, string name, byte hours, decimal tPay, string message)>();
            foreach (var itemPath in path)
            {
                using (TextFieldParser parser = new TextFieldParser(itemPath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        
                        date = DateTime.Parse(fields[0]);
                        name = fields[1];
                        hours = byte.Parse(fields[2]);
                        message = fields[3];
                    
                        role = a.RoleIdentification(name);
                        totalPay = a.SalaryCalc(hours, role);
                        tupleData.Add((date.Value, name, hours, totalPay, message));
                    }
                    tupleData.Sort();
                }
            }            
            return tupleData;
        }
        public static string Reader(string path)
        {
            string str = null;
            if (Directory.Exists(@"..\..\..\Files"))
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        str = sr.ReadToEnd();
                    }
                }
                               
                else
                {
                    using (File.Create(path))
                    using (StreamReader sr = new StreamReader(path))
                    {                        
                        str = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(@"..\..\..\Files");
                Reader(path);
            }
            return str;
        }
        public static void Writer(string path, DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }
        
        public static void EmployeeListWriter(string name, string role)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(employeeListFile, true))
            {
                employeeListStreamWriter.WriteLine($"{name}, {role}");
            }
        }
    }  
}
