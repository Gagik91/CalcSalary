﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSalary
{
    class ActionsOfEmployees
    {
        public static DateTime today = DateTime.Today;
        public static decimal TotalPay = 0;
        public static string whatWorkWas;
        public static decimal SalaryCalcManager(byte hr)
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
        public static decimal SalaryCalcEmployee(byte hr)
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
        public static decimal SalaryCalcFreelancer(byte hr)
        {
            decimal payPerHour = Settings.Freelancer.PayPerHour;
            decimal totalPay = 0;

            totalPay += hr * payPerHour;

            TotalPay = totalPay;
            return TotalPay;
        }

        public static void AddMan(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        {
            SalaryCalcManager(hours);
            if (dt is null)
            {
                dt = DateTime.Now;
            }
            Manager m = new Manager(name, new List<TimeRecord>()
            {
                    new TimeRecord(dt.Value.AddDays(0), name, hours, message, TotalPay, "manager", newMan)
            });
            Manager.manager.Add(m);
        }
        public static void AddEmp(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        {
            SalaryCalcEmployee(hours);
            if (dt is null)
            {
                dt = DateTime.Now;
            }
            Employee e = new Employee(name, new List<TimeRecord>()
            {
                new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", TotalPay, "employee", newMan)
            });
            Employee.employee.Add(e);
        }
        public static void AddFree(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        {
            SalaryCalcFreelancer(hours);
            if (dt is null)
            {
                dt = DateTime.Now;
            }
            Freelancer f = new Freelancer(name, new List<TimeRecord>()
            {
                new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", TotalPay, "freelancer", newMan)
            });
            Freelancer.freelancer.Add(f);
        }

        public static void AddEmployee(string name, byte hours = 0, byte selected = 0, bool addNew = false)
        {
            switch (selected)
            {
                case 1:
                    { AddMan(name, hours, newMan: addNew); }
                    break;
                case 2:
                    { AddEmp(name, hours, newMan: addNew); }
                    break;
                case 3:
                    { AddFree(name, hours, newMan: addNew); }
                    break;
                default:
                    break;
            }
        }

        public static void AddHours(string name = "", bool manager = false)
        {
            string nameEmp;
            if (manager)
            {
                Console.Write("Укажите имя сотрудника для добавления часов работы: ");
                nameEmp = Console.ReadLine();
                if (Manager.manager.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    nameEmp = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).FirstOrDefault().Name;
                    Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020)");
                    TimeSpan day = new TimeSpan(-1); 

                    while (day.TotalDays < 0 || day.TotalDays > 365)
                    {
                        try
                        {
                            Console.WriteLine("Часы можно добавить в любой день в пределах года:");
                            day = today - DateTime.Parse(Console.ReadLine());
                        }                        
                        catch 
                        { continue; }
                    }

                    Console.WriteLine($"Введите количество часов для добавления сотруднику {nameEmp}");
                    byte hr = 0;
                    while (hr < 1 || hr > 24)
                    {
                        try
                        {
                            Console.WriteLine("Количество часов можно ввести в пределах суток от 1 часа и до 24 часов:");
                            hr = byte.Parse(Console.ReadLine());
                        }
                        catch  
                        { continue; }
                    }

                    Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                    whatWorkWas = Console.ReadLine();
                    foreach (var item in Manager.manager)
                    {
                        if (item.Name.ToLower() == nameEmp.ToLower())
                        {
                            TotalPay = SalaryCalcManager(hr);
                            item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "manager"));
                            Files.ManagerWriter(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas);
                        }
                    }
                    Statistics.DisplayStats(name, true);
                }

                else if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    nameEmp = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).FirstOrDefault().Name;
                    Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020)");
                    TimeSpan day = new TimeSpan(-1);

                    while (day.TotalDays < 0 || day.TotalDays > 365)
                    {
                        try
                        {
                            Console.WriteLine("Часы можно добавить в любой день в пределах года:");
                            day = today - DateTime.Parse(Console.ReadLine());
                        }
                        catch
                        { continue; }
                    }

                    Console.WriteLine($"Введите количество часов для добавления сотруднику {nameEmp}");
                    byte hr = 0;
                    while (hr < 1 || hr > 24)
                    {
                        try
                        {
                            Console.WriteLine("Количество часов можно ввести в пределах суток от 1 часа и до 24 часов:");
                            hr = byte.Parse(Console.ReadLine());
                        }
                        catch
                        { continue; }
                    }

                    Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                    whatWorkWas = Console.ReadLine();
                    foreach (var item in Employee.employee)
                    {
                        if (item.Name.ToLower() == nameEmp.ToLower())
                        {
                            TotalPay = SalaryCalcEmployee(hr);
                            item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "employee"));
                            Files.EmployeeWriter(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas);
                        }
                    }
                    Statistics.DisplayStats(name, true);
                }

                else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    nameEmp = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).FirstOrDefault().Name;
                    Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020)");
                    TimeSpan day = new TimeSpan(-1);
                    while (day.TotalDays < 0 || day.TotalDays > 365)
                    {
                        try
                        {
                            Console.WriteLine("Часы можно добавить в любой день в пределах года:");
                            day = today - DateTime.Parse(Console.ReadLine());
                        }
                        catch
                        { continue; }
                    }

                    Console.WriteLine($"Введите количество часов для добавления сотруднику {nameEmp}");
                    byte hr = 0;
                    while (hr < 1 || hr > 24)
                    {
                        try
                        {
                            Console.WriteLine("Количество часов можно ввести в пределах суток от 1 часа и до 24 часов:");
                            hr = byte.Parse(Console.ReadLine());
                        }
                        catch
                        { continue; }
                    }

                    Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                    whatWorkWas = Console.ReadLine();
                    foreach (var item in Freelancer.freelancer)
                    {
                        if (item.Name.ToLower() == nameEmp.ToLower())
                        {
                            TotalPay = SalaryCalcFreelancer(hr);
                            item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "freelancer"));
                            Files.FreelancerWriter(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas);
                        }
                    }
                    Statistics.DisplayStats(name, true);
                }
            }
            nameEmp = name;
            if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020)");
                TimeSpan day = new TimeSpan(-1);
                while (day.TotalDays < 0 || day.TotalDays > 365)
                {
                    try
                    {
                        Console.WriteLine("Часы можно добавить в любой день в пределах года:");
                        day = today - DateTime.Parse(Console.ReadLine());
                    }
                    catch
                    { continue; }
                }

                Console.WriteLine($"Введите количество часов для добавления");
                byte hr = 0;
                while (hr < 1 || hr > 24)
                {
                    try
                    {
                        Console.WriteLine("Количество часов можно ввести в пределах суток от 1 часа и до 24 часов:");
                        hr = byte.Parse(Console.ReadLine());
                    }
                    catch
                    { continue; }
                }

                Console.WriteLine($"Укажите какую работу выполняли: ");
                whatWorkWas = Console.ReadLine();
                foreach (var item in Employee.employee)
                {
                    if (item.Name.ToLower() == nameEmp.ToLower())
                    {
                        TotalPay = SalaryCalcEmployee(hr);
                        item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "employee"));
                        Files.EmployeeWriter(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas);
                    }
                }
            }
            else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                Console.WriteLine($"Укажите в какой день добавить часы (шаблон: 01.01.2020) самое ранее за {DateTime.Now.AddDays(-2).ToShortDateString()}: ");
                TimeSpan day = new TimeSpan(-1);
                while (day.TotalDays < 0 || day.TotalDays > 2)
                {
                    try
                    {
                        Console.WriteLine("Нужно ввести часы в пределах 2 дней:");
                        day = today - DateTime.Parse(Console.ReadLine());
                    }
                    catch
                    { continue; }
                }

                Console.WriteLine($"Введите количество часов для добавления");
                byte hr = 0;
                while (hr < 1 || hr > 24)
                {
                    try
                    {
                        Console.WriteLine("Количество часов можно ввести в пределах суток от 1 часа и до 24 часов:");
                        hr = byte.Parse(Console.ReadLine());
                    }
                    catch
                    { continue; }
                }

                var nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower());
                Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                whatWorkWas = Console.ReadLine();
                foreach (var item in Freelancer.freelancer)
                {
                    if (item.Name.ToLower() == nameEmp.ToLower())
                    {
                        TotalPay = SalaryCalcFreelancer(hr);
                        item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "freelancer"));
                        Files.FreelancerWriter(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas);
                    }
                }
            }
        }
    }
}
