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

        public static void ActionMenu()
        {
            bool exit = false;

            Console.Write("Введите Ваше имя: ");
            string name = Console.ReadLine();
            ActionsOfEmployees actionOfEmp = new ActionsOfEmployees();
            Settings.Role? role = actionOfEmp.RoleIdentification(name);
            byte selectedAction;
            Statistics stat = new Statistics();

            while (role == 0)
            {
                Console.Write($"Сотрудник с именем {name} не найден, нажмите 1 для выхода или введите имя: ");
                name = Console.ReadLine();
                Console.WriteLine("\n");
                if (name == "1")
                {
                    exit = true;
                    break;
                }
                else
                {
                    role = actionOfEmp.RoleIdentification(name);
                }
            }

            Console.WriteLine($"Здравствуйте, {name}!");
            Console.WriteLine($"Ваша роль: {role}");
            while (!exit)
            {
                Console.WriteLine("\nВыберите желаемое действие:\n");

                switch (role)
                {
                    case Settings.Role.Manager:
                        {
                            Console.WriteLine("(1) Добавить сотрудника: ");
                            Console.WriteLine("(2) Просмотреть отчет по всем сотрудникам: ");
                            Console.WriteLine("(3) Просмотреть отчет по конкретному сотруднику: ");
                            Console.WriteLine("(4) Добавить часы работы: ");
                            Console.WriteLine("(5) Выход из программы: ");
                            selectedAction = 0;
                            while (selectedAction < 1 || selectedAction > 5)
                            {
                                Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                string temp = Console.ReadLine();
                                byte.TryParse(temp, out selectedAction);
                                Console.WriteLine("\n");
                            }
                            switch (selectedAction)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Укажите должность для добавления сотрудника: ");
                                        Console.WriteLine("Руководитель - нажмите 1: ");
                                        Console.WriteLine("Сотрудник на зарплате - нажмите 2: ");
                                        Console.WriteLine("Внештатный сотрудник - нажмите 3: ");
                                        Settings.Role selectedEmployee = 0;
                                        while (selectedEmployee < (Settings.Role)1 || selectedEmployee > (Settings.Role)3)
                                        {
                                            Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                            string temp = Console.ReadLine();
                                            Enum.TryParse(temp, out selectedEmployee);
                                            Console.WriteLine("\n");
                                        }

                                        Console.Write("Укажите имя для добавления сотрудника: ");
                                        string nameEmployee = Console.ReadLine();
                                        actionOfEmp.AddEmployee(nameEmployee, selectedRole: selectedEmployee);
                                    }
                                    break;
                                case 2:
                                    {
                                        stat.DisplayAllStats();
                                    }
                                    break;
                                case 3:
                                    {
                                        stat.DisplayStats(name, role.Value);
                                    }
                                    break;
                                case 4:
                                    {
                                        actionOfEmp.AddHours(name, role.Value);
                                    }
                                    break;
                                case 5:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    case Settings.Role.Employee:
                        {
                            Console.WriteLine("(1) Добавить часы работы: ");
                            Console.WriteLine("(2) Просмотреть отчет по отработанным часам и зарплате за период: ");
                            Console.WriteLine("(3) Выход из программы: ");
                            selectedAction = 0;
                            while (selectedAction < 1 || selectedAction > 3)
                            {
                                Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                string temp = Console.ReadLine();
                                byte.TryParse(temp, out selectedAction);
                                Console.WriteLine("\n");
                            }
                            switch (selectedAction)
                            {
                                case 1:
                                    {
                                        actionOfEmp.AddHours(name, role.Value);
                                    }
                                    break;
                                case 2:
                                    { stat.DisplayStats(name, role.Value); }
                                    break;
                                case 3:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    case Settings.Role.Freelancer:
                        {
                            Console.WriteLine("(1) Добавить часы работы: ");
                            Console.WriteLine("(2) Просмотреть отчет по отработанным часам и зарплате за период: ");
                            Console.WriteLine("(3) Выход из программы:");
                            selectedAction = 0;
                            while (selectedAction < 1 || selectedAction > 3)
                            {
                                Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                string temp = Console.ReadLine();
                                byte.TryParse(temp, out selectedAction);
                                Console.WriteLine("\n");
                            }

                            switch (selectedAction)
                            {
                                case 1:
                                    {
                                        actionOfEmp.AddHours(name, role.Value);
                                    }
                                    break;
                                case 2:
                                    { stat.DisplayStats(name, role.Value); }
                                    break;
                                case 3:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }


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
            else if (hr > 8 && role != Settings.Role.Freelancer)
            {
                totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
            }
                        
            return totalPay;
        }

        public void AddEmployee(string name, Settings.Role selectedRole)
        {
            Person allCurrentData = new Person()
            {
                Date = DateTime.Now.AddDays(0),
                Name = name,
                Hours = 0,
                Message = "First Day",
                Role = selectedRole
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
            Person allCurrentData = new Person();

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
                allCurrentData.Date = DateTime.Now.AddDays(-day.Days);
                allCurrentData.Name = nameEmp;
                allCurrentData.Hours = hr;
                allCurrentData.Message = whatWorkWas;
                
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

        public List<Person> AllData()
        {
            List<Person> allData = new List<Person>();
            Files files = new Files();
            List<string> listOfPaths = files.AllPathsIdentification();
            allData = files.Reader(listOfPaths);
            return allData;
        }

        public List<Person> FilteredByDate(DateTime startDate)
        {
            List<Person> allData = AllData();
            List<Person> filteredByDate = allData.Where(n => n.Date >= startDate).ToList();

            return filteredByDate;
        }

        public List<Person> FilteredByName(string name, List<Person> data = null)
        {
            List<Person> filteredByName = new List<Person>();
            if (data is null)
            {
                List<Person> allData = AllData();
                filteredByName = allData.Where(n => n.Name.ToLower() == name.ToLower()).ToList();
            }
            else
            {
                List<Person> allData = data;
                filteredByName = allData.Where(n => n.Name.ToLower() == name.ToLower()).ToList();
            }
            return filteredByName;
        }

        public List<Person> HoursAndPaySummedByName(string acceptedName = null, List<Person> acceptedData = null)
        {
            List<Person> summedByName = new List<Person>();
            List<Person> data = new List<Person>();

            if (acceptedData is null)
            {
                data = (acceptedName is null) ? data = AllData() : data = FilteredByName(acceptedName);
            }

            else
            {
                data = (acceptedName is null) ? data = acceptedData : data = FilteredByName(acceptedName, acceptedData);
            }
            summedByName = data
                            .GroupBy(n => n.Name.ToLower())
                            .Select(t => new Person
                            {
                                Name = t.Key,
                                Hours = (byte)t
                            .Sum(t => t.Hours),
                                Pay = t
                            .Sum(t => t.Pay)
                            })
                            .Where(n => n.Hours > 0)
                            .ToList();

            return summedByName;
        }
    }
}
