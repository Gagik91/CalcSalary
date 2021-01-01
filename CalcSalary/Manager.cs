using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{    
    public class Manager: Staff
    {
        public static List<Manager> manager = new List<Manager>();
        public decimal TotalPay { get; set; }
        public Manager(string name, List<TimeRecord> timeRecords) : base(name, Settings.Manager.MonthSalary, timeRecords)
        {
            AddPay(timeRecords);           
        }

        public void AddPay(List<TimeRecord> timeRecords, byte hr = 0)
        {
            decimal payPerHour = Settings.Manager.MonthSalary / Settings.WorkHoursInMonth;
            decimal totalPay = 0;
            decimal bonusPerDay = (Settings.Manager.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;

            foreach (var timeRecord in timeRecords)
            {
                if (timeRecord.Hours <= Settings.WorkHourInDay)
                {
                    totalPay += timeRecord.Hours * payPerHour;
                }
                else // переработка
                {
                    totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
                }
            }
            TotalPay += totalPay;
        }

        public static void PrintHello(string name)
        {
            Console.WriteLine($"Здравствуйте, {name}!");
            Console.WriteLine("Ваша роль: руководитель");
            Console.WriteLine("Выберите желаемое действие:\n");
            Console.WriteLine("(1) Добавить сотрудника: ");
            Console.WriteLine("(2) Просмотреть отчет по всем сотрудникам: ");
            Console.WriteLine("(3) Просмотреть отчет по конкретному сотруднику: ");
            Console.WriteLine("(4) Добавить часы работы: ");
            Console.WriteLine("(5) Выход из программы: \n");

            byte selectedAction = byte.Parse(Console.ReadLine());
            while (selectedAction < 1 || selectedAction > 5)
            {
                try
                {
                    Console.WriteLine("Нужно ввести цифры из предложенных вариантов");
                    selectedAction = byte.Parse(Console.ReadLine());
                }
                catch { }
            }

            switch (selectedAction)
            {
                case 1:
                    {
                        Console.WriteLine("Укажите должность для добавления сотрудника: ");
                        Console.WriteLine("Руководитель - нажмите 1: ");
                        Console.WriteLine("Сотрудник на зарплате - нажмите 2: ");
                        Console.WriteLine("Внештатный сотрудник - нажмите 3: ");
                        byte selectedEmployee = byte.Parse(Console.ReadLine());
                        Console.WriteLine("Укажите имя для добавления сотрудника: ");
                        string nameEmployee = Console.ReadLine();
                        AddEmployee(nameEmployee, selected: selectedEmployee, addNew: true);
                    }
                    break;
                case 2:
                    {
                        Statistics.DisplayAllStats();
                    }
                    break;
                case 3:
                    {
                        Statistics.DisplayStats(manager: true);
                    }
                    break;
                case 4:
                    {
                        Statistics.AddHours(name,true);
                    }
                    break;
                case 5:
                    { }
                    break;
                default:
                    break;
            }
        }
                
        public static void AddEmployee(string name, byte hours = 0, byte selected = 0, bool addNew = false)
        {            
            switch (selected)
            {
                case 1:
                    {
                        AddMan(name, hours, newMan: addNew);                                
                    }
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
        public static void AddMan(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        {
            Statistics.SalaryCalcManager(hours);
            if (dt is null)
            {
                dt = DateTime.Now;
            }
            Manager m = new Manager(name, new List<TimeRecord>()
            {
                    new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", Statistics.TotalPay, "manager", newMan)
            });
            manager.Add(m);
        }
        public static void AddEmp(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        {
            Statistics.SalaryCalcEmployee(hours);
            if (dt is null)
            {
                dt = DateTime.Now;
            }
            Employee e = new Employee(name, new List<TimeRecord>()
            { 
                new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", Statistics.TotalPay, "employee", newMan)
            });
            Employee.employee.Add(e);
        }
        public static void AddFree(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        {
            Statistics.SalaryCalcFreelancer(hours);
            if (dt is null)
            {
                dt = DateTime.Now;
            }
            Freelancer f = new Freelancer(name, new List<TimeRecord>()
            {
                new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", Statistics.TotalPay, "freelancer", newMan)
            });
            Freelancer.freelancer.Add(f);
        }
    }
}
