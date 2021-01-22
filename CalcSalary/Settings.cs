using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{    
    public class Settings
    {
        public enum Role
        {
            Manager = 1,
            Employee,
            Freelancer
        }

        public class Manager
        {
            public const decimal MonthBonus = 20000;
            public const decimal MonthSalary = 200000;
        }
        public class Employee
        {
            public const decimal MonthBonus = 20000;
            public const decimal MonthSalary = 120000;
        }
        public class Freelancer
        {
            public const decimal PayPerHour = 1000;
        }

        /// <summary>
        /// Количество рабочих часов в месяце
        /// </summary>
        public const byte WorkHoursInMonth = 160;

        /// <summary>
        /// рабочие часы в день
        /// </summary>
        public const byte WorkHourInDay = 8;

        public string PathIdentification(string name)
        {
            string path = null;
            using (TextFieldParser parser = new TextFieldParser(Files.employeeListFile))
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
                            path = Files.manFile;
                        }
                        else if (fields[1].ToLower() == "Employee".ToLower())
                        {
                            path = Files.empFile;
                        }
                        else if (fields[1].ToLower() == "Freelancer".ToLower())
                        {
                            path = Files.freeFile;
                        }
                        break;
                    }
                }
            }
            return path;
        }
    }
}
