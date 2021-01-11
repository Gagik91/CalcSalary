using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSalary
{
    class ActionsOfEmployees
    {
        private static DateTime today = DateTime.Today;
        private static decimal TotalPay = 0;
        private static string whatWorkWas;
        public static decimal SalaryCalc(byte hr, Settings.Roles role)
        {

            switch (role)
            {
                case Settings.Roles.Manager:
                    {
                        decimal payPerHour = Settings.Manager.MonthSalary / Settings.WorkHoursInMonth;
                        decimal totalPay = 0;
                        decimal bonusPerDay = (Settings.Manager.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;

                        if (hr <= Settings.WorkHourInDay)
                        {
                            totalPay += hr * payPerHour;
                        }
                        else // переработка
                        {
                            totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
                        }

                        TotalPay = totalPay;
                        return TotalPay;
                    }
                case Settings.Roles.Employee:
                    {
                        decimal payPerHour = Settings.Employee.MonthSalary / Settings.WorkHoursInMonth;
                        decimal totalPay = 0;
                        decimal bonusPerDay = (Settings.Employee.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;

                        if (hr <= Settings.WorkHourInDay)
                        {
                            totalPay += hr * payPerHour;
                        }
                        else // переработка
                        {
                            totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
                        }

                        TotalPay = totalPay;
                        return TotalPay;
                    }
                case Settings.Roles.Freelancer:
                    {
                        decimal payPerHour = Settings.Freelancer.PayPerHour;
                        decimal totalPay = 0;

                        totalPay += hr * payPerHour;

                        TotalPay = totalPay;
                        return TotalPay;
                    }
            }
            return TotalPay;
        }

        public static void AddEmployee(string name, Settings.Roles selectedRole, byte hours = 0)
        {
            using (ContextWithDB db = new ContextWithDB())
            {
                //Проверка отсутствует ли сотрудник с таким именем в БД, при отсутствии заполнить информацию
                if (!db.AllEmployeesHoursWorkedList.Any(n => n.Name == name))
                {
                    SalaryCalc(hours, selectedRole);

                    Console.WriteLine($"\n\nУкажите с какого дня работает {name} (шаблон: 01.01.2020)");
                    TimeSpan day = new TimeSpan(-1);

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

                    Console.WriteLine($"\n\nВведите количество часов для отработанных {name}, при отсутствии введите - 0");
                    int hr = -1;
                    while (hr < 0 || hr > 24)
                    {
                        try
                        {
                            Console.Write("Количество часов можно ввести в пределах от 0 и до 24 часов: ");
                            hr = byte.Parse(Console.ReadLine());
                        }
                        catch
                        { continue; }
                    }

                    Console.WriteLine($"\n\nУкажите какую работу выполнял {name}, при отсутствии можете указать - \"First day\"");
                    whatWorkWas = Console.ReadLine();

                    TotalPay = SalaryCalc((byte)hr, selectedRole);
                    AllEmployeesHoursWorkedList.AddEmployee(selectedRole, name, DateTime.Now.AddDays(-day.Days), (byte)hr, whatWorkWas);
                }
                else
                {
                    Console.WriteLine($"\n\nСотрудник {name} уже работает в компании\n");
                }
            }
        }

        public static void AddHours(string name = "")
        {
            using (ContextWithDB db = new ContextWithDB())
            {
                string nameEmp;
                Settings.Roles empRole = db.AllEmployeesHoursWorkedList.FirstOrDefault(n => n.Name.ToLower() == name.ToLower()).RoleEmployee;
                Settings.Roles addHoursRole = 0;
                if (empRole == Settings.Roles.Manager)
                {
                    Console.Write("Укажите имя сотрудника для добавления часов работы: ");
                    nameEmp = Console.ReadLine();
                    if (db.AllEmployeesHoursWorkedList.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                    {
                        nameEmp = db.AllEmployeesHoursWorkedList.Where(n => n.Name.ToLower() == nameEmp.ToLower()).FirstOrDefault().Name;   //чтение и присвоение имени сотрудника из БД
                        addHoursRole = db.AllEmployeesHoursWorkedList.FirstOrDefault(n => n.Name.ToLower() == name.ToLower()).RoleEmployee; //чтение и присвоение роли найденного сотрудника из БД

                        Console.WriteLine("\n\nУкажите в какой день добавить часы (шаблон: 01.01.2020)");
                        TimeSpan day = new TimeSpan(-1);

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

                        Console.WriteLine($"\n\nВведите количество часов для добавления сотруднику {nameEmp}");
                        byte hr = 0;
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

                        Console.WriteLine($"\n\nУкажите какую работу выполнял {nameEmp}");
                        whatWorkWas = Console.ReadLine();

                        TotalPay = SalaryCalc(hr, empRole);
                        AllEmployeesHoursWorkedList.AddEmployee(addHoursRole, nameEmp, DateTime.Now.AddDays(-day.Days), hr, whatWorkWas);
                    }
                    else
                    {
                        Console.WriteLine("\n\nСотрудник с таким именем отсутствует\n");
                    }
                    return;
                }
                

                nameEmp = name;
                if (empRole == Settings.Roles.Employee)
                {
                    Console.WriteLine("\n\nУкажите в какой день добавить часы (шаблон: 01.01.2020)");
                    TimeSpan day = new TimeSpan(-1);
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

                    Console.WriteLine($"\n\nВведите количество часов для добавления");
                    byte hr = 0;
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

                    Console.WriteLine($"\n\nУкажите какую работу выполняли:");
                    whatWorkWas = Console.ReadLine();

                    TotalPay = SalaryCalc(hr, empRole);
                    AllEmployeesHoursWorkedList.AddEmployee(empRole, nameEmp, DateTime.Now.AddDays(-day.Days), hr, whatWorkWas);
                }
                else if (empRole == Settings.Roles.Freelancer)
                {
                    Console.WriteLine($"\n\nУкажите в какой день добавить часы (шаблон: 01.01.2020) самое ранее за {DateTime.Now.AddDays(-2).ToShortDateString()}: ");
                    TimeSpan day = new TimeSpan(-1);
                    while (day.TotalDays < 0 || day.TotalDays > 2)
                    {
                        try
                        {
                            Console.Write("Нужно ввести часы в пределах 2 дней: ");
                            day = today - DateTime.Parse(Console.ReadLine());
                        }
                        catch
                        { continue; }
                    }

                    Console.WriteLine($"\n\nВведите количество часов для добавления");
                    byte hr = 0;
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

                    Console.WriteLine($"\n\nУкажите какую работу выполняли:");
                    whatWorkWas = Console.ReadLine();
                    TotalPay = SalaryCalc(hr, empRole);
                    AllEmployeesHoursWorkedList.AddEmployee(empRole, nameEmp, DateTime.Now.AddDays(-day.Days), hr, whatWorkWas);
                }

                else
                {
                    Console.WriteLine("\n\nСотрудник с таким именем отсутствует\n");
                }
            }
        }
    }
}
