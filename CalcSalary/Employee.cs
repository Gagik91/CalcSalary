using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Employee: Staff
    {
        public static List<Employee> employee = new List<Employee>();
        public decimal TotalPay { get; set; }
        public Employee(string name, List<TimeRecord> timeRecords) : base(name, Settings.Employee.MonthSalary, timeRecords)
        {
            AddPay(timeRecords);
        }

        public void AddPay(List<TimeRecord> timeRecords, byte hr = 0)
        {
            decimal payPerHour = Settings.Employee.MonthSalary / Settings.WorkHoursInMonth;
            decimal totalPay = 0;
            decimal bonusPerDay = (Settings.Employee.MonthSalary / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;

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
            Console.WriteLine("Ваша роль: Сотрудник на зарплате");
            Console.WriteLine("Выберите желаемое действие:\n");
            Console.WriteLine("(1) Добавить часы работы: ");
            Console.WriteLine("(2) Просмотреть отчет по отработанным часам и зарплате за период: ");
            Console.WriteLine("(3) Выход из программы: \n");

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
                        Statistics.AddHours(name, false);
                    }
                    break;
                case 2:
                    {
                        Statistics.DisplayStats(name, false);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
