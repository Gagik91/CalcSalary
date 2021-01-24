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
            switch (role)
            {
                case Settings.Role.Manager:
                    {
                        payPerHour = Settings.Manager.MonthSalary / Settings.WorkHoursInMonth;
                        bonusPerDay = (Settings.Manager.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;
                    }
                    break;
                case Settings.Role.Employee:
                    {
                        payPerHour = Settings.Employee.MonthSalary / Settings.WorkHoursInMonth;
                        bonusPerDay = (Settings.Employee.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;
                    }
                    break;
                case Settings.Role.Freelancer:
                    {
                        payPerHour = Settings.Freelancer.PayPerHour;
                        totalPay += hr * payPerHour;
                    }
                    break;
                default:
                    break;
            }

            if (hr <= Settings.WorkHourInDay && role != Settings.Role.Freelancer)
            {
                totalPay += hr * payPerHour;
            }
            else
            {
                totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
            }
                        
            return totalPay;
        }

        public void AddEmployee(string name, Settings.Role selectedRole)
        {
            AllCurrentData allCurrentData = new AllCurrentData()
            {
                date = DateTime.Now.AddDays(0),
                name = name,
                hours = 0,
                message = "First Day",
                role = selectedRole
            };
            switch (selectedRole)
            {
                case Settings.Role.Manager:
                    {
                        files.Writer(Files.manFile, allCurrentData, false);
                        files.Writer(Files.manFile, allCurrentData, true);
                    }
                    break;
                case Settings.Role.Employee:
                    {
                        files.Writer(Files.empFile, allCurrentData, false);
                        files.Writer(Files.empFile, allCurrentData, true);
                    }
                    break;
                case Settings.Role.Freelancer:
                    {
                        files.Writer(Files.freeFile, allCurrentData, false);
                        files.Writer(Files.freeFile, allCurrentData, true);
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
            string path = files.PathIdentification(name);
            byte hr = 0;
            AllCurrentData allCurrentData = new AllCurrentData();

            switch (role)
            {
                case Settings.Role.Manager:
                    {
                        Console.Write("Укажите имя сотрудника для добавления часов работы: ");
                        nameEmp = Console.ReadLine();
                        path = files.PathIdentification(nameEmp);
                        if (path is null)
                        {
                            Console.WriteLine("\n\nСотрудник с таким именем отсутствует\n");
                            return;
                        }
                    }
                    break;
                case Settings.Role.Employee:
                    {
                        nameEmp = name;
                        path = files.PathIdentification(nameEmp);
                    }
                    break;
                case Settings.Role.Freelancer:
                    {
                        nameEmp = name;
                        path = files.PathIdentification(nameEmp);
                    }
                    break;
                default:
                    break;
            }
                        
            TimeSpan day = new TimeSpan(-1);
            if (role != Settings.Role.Freelancer)
            {
                Console.WriteLine("\n\nУкажите в какой день добавить часы (шаблон: 01.01.2020)");

                while (day.TotalDays < 0 || day.TotalDays > 365)
                {
                    Console.Write("Часы можно добавить в любой день в пределах года: ");
                    string temp = Console.ReadLine();
                    DateTime date;
                    DateTime.TryParse(temp, out date);
                    Console.WriteLine("\n");
                    day = today - date;
                }
            }

            else if (role == Settings.Role.Freelancer)
            {
                Console.WriteLine($"\n\nУкажите в какой день добавить часы (шаблон: 01.01.2020) самое ранее за {DateTime.Now.AddDays(-2).ToShortDateString()}: ");
                while (day.TotalDays < 0 || day.TotalDays > 2)
                {
                    Console.Write("Нужно ввести день в пределах последних 2 суток: ");
                    string temp = Console.ReadLine();
                    DateTime date;
                    DateTime.TryParse(temp, out date);
                    Console.WriteLine("\n");
                    day = today - date;
                }
            }

            Console.WriteLine($"\n\nВведите количество часов для добавления");
            while (hr < 1 || hr > 24)
            {
                Console.Write("Количество часов можно ввести в пределах суток от 1 часа и до 24 часов: ");
                string temp = Console.ReadLine();
                byte.TryParse(temp, out hr);
                Console.WriteLine("\n");
            }

            Console.WriteLine($"\n\nУкажите выполненную работу");
            whatWorkWas = Console.ReadLine();

            if (path is not null)
            {
                allCurrentData.date = DateTime.Now.AddDays(-day.Days);
                allCurrentData.name = nameEmp;
                allCurrentData.hours = hr;
                allCurrentData.message = whatWorkWas;
                
                files.Writer(path, allCurrentData, false);
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
                        { role = Settings.Role.Manager; }

                        else if (fields[1].ToLower() == "Employee".ToLower())
                        { role = Settings.Role.Employee; }

                        else if (fields[1].ToLower() == "Freelancer".ToLower())
                        { role = Settings.Role.Freelancer; }
                        break;
                    }                           
                }
            }
            return role.GetValueOrDefault();
        }

        public List<AllCurrentData> AllData()
        {
            List<AllCurrentData> allData = new List<AllCurrentData>();
            Files files = new Files();
            List<string> listOfPaths = files.AllPathsIdentification();
            allData = files.Reader(listOfPaths);
            return allData;
        }
        public List<AllCurrentData> FilteredByDate(DateTime startDate)
        {
            List<AllCurrentData> allData = AllData();
            List<AllCurrentData> filteredByDate = allData.Where(n => n.date >= startDate).ToList();

            return filteredByDate;
        }
        public List<AllCurrentData> FilteredByName(string name, List<AllCurrentData> data = null)
        {
            List<AllCurrentData> filteredByName = new List<AllCurrentData>();
            if (data is null)
            {
                List<AllCurrentData> allData = AllData();
                filteredByName = allData.Where(n => n.name.ToLower() == name.ToLower()).ToList();    
            }
            else
            {
                List<AllCurrentData> allData = data;
                filteredByName = allData.Where(n => n.name.ToLower() == name.ToLower()).ToList();
            }
            return filteredByName;
        }
        public List<AllCurrentData> HoursAndPaySummedByName(string acceptedName = null, List<AllCurrentData> acceptedData = null)
        {
            List<AllCurrentData> summedByName = new List<AllCurrentData>();
            List<AllCurrentData> data = new List<AllCurrentData>();
            
            if (acceptedData is null)
            {
                _ = acceptedName is null ? data = AllData() : data = FilteredByName(acceptedName);
            }

            else 
            {
                _ = acceptedName is null ? data = acceptedData : data = FilteredByName(acceptedName, acceptedData);
            }

            summedByName = data
                            .GroupBy(n => n.name.ToLower())
                            .Select(t => new AllCurrentData
                            {
                                name = t.Key,
                                hours = (byte)t
                            .Sum(t => t.hours),
                                pay = t
                            .Sum(t => t.pay)
                            })
                            .Where(n => n.hours > 0)
                            .ToList();
            return summedByName;
        }
    }
}
