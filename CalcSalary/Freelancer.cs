using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Freelancer : Person
    {
        public decimal TotalPay { get; set; }
        public static List<Freelancer> freelancer = new List<Freelancer>();
        public Freelancer(string name, List<TimeRecord> timeRecords) : base(name, timeRecords)
        {
            AddPay(timeRecords);
        }
        public void AddPay(List<TimeRecord> timeRecords, byte hr = 0)
        {
            decimal payPerHour = Settings.Freelancer.PayPerHour;
            decimal totalPay = 0;

            foreach (var timeRecord in timeRecords)
            {
                totalPay += timeRecord.Hours * payPerHour;
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
