using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSalary
{
    class ActionsOfEmployees
    {
        private DateTime today = DateTime.Today;
        Files files = new Files();
        public decimal SalaryCalc(byte hr, Settings.Role role)
        {
            decimal payPerHour = 0;
            decimal totalPay = 0;
            decimal bonusPerDay = 0;
            
            if (role == Settings.Role.Manager)
            {
                payPerHour = Settings.Manager.MonthSalary / Settings.WorkHoursInMonth;
                bonusPerDay = (Settings.Manager.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;
            }
            else if (role == Settings.Role.Employee)
            {
                payPerHour = Settings.Employee.MonthSalary / Settings.WorkHoursInMonth;
                bonusPerDay = (Settings.Employee.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;
            }
            else if (role == Settings.Role.Freelancer)
            {
                payPerHour = Settings.Freelancer.PayPerHour;
                totalPay += hr * payPerHour;
            }

            if (hr <= Settings.WorkHourInDay && (role == Settings.Role.Manager || role == Settings.Role.Employee))
            {
                totalPay += hr * payPerHour;
            }
            else if(hr > Settings.WorkHourInDay && (role == Settings.Role.Manager || role == Settings.Role.Employee))// переработка
            {
                totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
            }
                        
            return totalPay;
        }

        public void AddEmployee(string name, Settings.Role selected, byte hours = 0)
        {
            switch (selected)
            {
                case Settings.Role.Manager:
                    { 
                        files.Writer(Files.manFile,DateTime.Now.AddDays(0),name,hours,"First Day");
                        files.EmployeeListWriter(name, "Manager");
                    }
                    break;
                case Settings.Role.Employee:
                    { 
                        files.Writer(Files.empFile, DateTime.Now.AddDays(0), name, hours, "First Day");
                        files.EmployeeListWriter(name, "Employee");
                    }
                    break;
                case Settings.Role.Freelancer:
                    { 
                        files.Writer(Files.freeFile, DateTime.Now.AddDays(0), name, hours, "First Day");
                        files.EmployeeListWriter(name, "Freelancer");
                    }
                    break;
                default:
                    break;
            }
        }

        public void AddHours(string name, Settings.Role role)
        {
            string whatWorkWas;
            string nameEmp = "";
            Settings.Role roleAddedHours;
            string path = "";
            byte hr = 0;
            if (role == Settings.Role.Manager)
            {
                Console.Write("Укажите имя сотрудника для добавления часов работы: ");
                nameEmp = Console.ReadLine();
                roleAddedHours = RoleIdentification(nameEmp);
                
                if (roleAddedHours == Settings.Role.Manager)
                {
                    path = Files.manFile;
                }
                else if (roleAddedHours == Settings.Role.Employee)
                {
                    path = Files.empFile;
                }
                else if (roleAddedHours == Settings.Role.Freelancer)
                {
                    path = Files.freeFile;
                }
                else
                {
                    Console.WriteLine("\n\nСотрудник с таким именем отсутствует\n");
                    return;
                }

            }
            else if (role == Settings.Role.Employee)
            {
                path = Files.empFile;
                nameEmp = name;
            }
            else if (role == Settings.Role.Freelancer)
            {
                path = Files.freeFile;
                nameEmp = name;
            }
            
            TimeSpan day = new TimeSpan(-1);
            if (role != Settings.Role.Freelancer)
            {
                Console.WriteLine("\n\nУкажите в какой день добавить часы (шаблон: 01.01.2020)");

                while (day.TotalDays < 0 || day.TotalDays > 365)
                {
                    try
                    {
                        Console.Write("Часы можно добавить в любой день в пределах года: ");
                        day = today - DateTime.Parse(Console.ReadLine());
                    }
                    catch
                    { continue; }
                }
            }

            else if (role == Settings.Role.Freelancer)
            {
                Console.WriteLine($"\n\nУкажите в какой день добавить часы (шаблон: 01.01.2020) самое ранее за {DateTime.Now.AddDays(-2).ToShortDateString()}: ");
                while (day.TotalDays < 0 || day.TotalDays > 2)
                {
                    try
                    {
                        Console.Write("Нужно ввести день в пределах последних 2 суток: ");
                        day = today - DateTime.Parse(Console.ReadLine());
                    }
                    catch
                    { continue; }
                }
            }

            Console.WriteLine($"\n\nВведите количество часов для добавления");
            while (hr < 1 || hr > 24)
            {
                try
                {
                    Console.Write("Количество часов можно ввести в пределах суток от 1 часа и до 24 часов: ");
                    hr = byte.Parse(Console.ReadLine());
                }
                catch
                { continue; }
            }

            Console.WriteLine($"\n\nУкажите выполненную работу");
            whatWorkWas = Console.ReadLine();

            if (path.Length > 0)
            {
                files.Writer(path, DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas);
            }                                        
        }

        public Settings.Role RoleIdentification(string name)
        {
            Settings.Role? role = null;
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
                            role = Settings.Role.Manager;
                        }
                        else if (fields[1].ToLower() == "Employee".ToLower())
                        {
                            role = Settings.Role.Employee;
                        }
                        else if (fields[1].ToLower() == "Freelancer".ToLower())
                        {
                            role = Settings.Role.Freelancer;
                        }
                        break;
                    }                           
                }
            }
            return role.GetValueOrDefault();
        }
    }
}
