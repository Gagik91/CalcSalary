using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.IO;
using System.Threading;

namespace CalcSalary
{
    class Statistics
    {
        public static byte period;
        public static DateTime today = DateTime.Today;

        //метод выбора периода
        public static void Stats(byte per = 0)
        {
            Console.WriteLine("Выберите период за который требуется просмотреть отчет");
            Console.WriteLine("Нажмите 1, чтобы просмотреть отчет за 1 день");
            Console.WriteLine("Нажмите 2, чтобы просмотреть отчет за неделю");
            Console.WriteLine("Нажмите 3, чтобы просмотреть отчет за месяц");
            period = 0;
            if (per >= 1 && per <= 3)
            {
                period = per;
                return;
            }
            while (period < 1 || period > 3)
            {
                try
                {
                    Console.Write("Нужно ввести цифры из предложенных вариантов: ");
                    period = byte.Parse(Console.ReadLine());
                    Console.WriteLine("\n");
                }
                catch { continue; }
            }
        }
        public static void CalcStatsOfAll(byte per = 0)
        {
            DateTime startDate = new DateTime();

            int hoursWorked = 0;
            decimal amountToPaid = 0;

            if (period == 1)
            {
                startDate = today.AddDays(-1);                
            }
            else if (period == 2)
            {
                startDate = today.AddDays(-7);
            }
            else if (period == 3)
            {
                startDate = today.AddMonths(-1);
            }

            foreach (var item in Manager.manager)
            {
                var tRecords = item.TimeRecords.Where(t => t.Date >= startDate);
                decimal tPay = 0;
                int sumHours = 0;
                foreach (var h in tRecords)
                {
                    tPay += h.TotalPay;
                    sumHours += h.Hours;
                }
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                Console.WriteLine("________");
                hoursWorked += sumHours;
                amountToPaid += tPay;
            }
            foreach (var item in Employee.employee)
            {
                var tRecords = item.TimeRecords.Where(t => t.Date >= startDate);
                decimal tPay = 0;
                var sumHours = 0;
                foreach (var h in tRecords)
                {
                    tPay += h.TotalPay;
                    sumHours += h.Hours;
                }
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                Console.WriteLine("________");
                hoursWorked += sumHours;
                amountToPaid += tPay;

            }
            foreach (var item in Freelancer.freelancer)
            {
                var tRecords = item.TimeRecords.Where(t => t.Date >= startDate);
                decimal tPay = 0;
                var sumHours = 0;
                foreach (var h in tRecords)
                {
                    tPay += h.TotalPay;
                    sumHours += h.Hours;
                }
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                Console.WriteLine("________");
                hoursWorked += sumHours;
                amountToPaid += tPay;
            }
            Person.TotalPay += amountToPaid;
            Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} \n");
        }

        //расчет статистики конкретного сотрудника по имени
        public static void CalcStats(string nameEmp, byte per = 0)
        {
            IEnumerable<Person> nm = null;
            DateTime startDate = new DateTime();
            Stats(per);
            if (period == 1)
            {
                startDate = today.AddDays(-1);
            }
            else if (period == 2)
            {
                startDate = today.AddDays(-7);
            }
            else if (period == 3)
            {
                startDate = today.AddMonths(-1);
            }

            if (Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
            {
                nm = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
            }
            else if (Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
            {
                nm = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
            }
            else if (Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
            {
                nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
            }
            else
            {
                Console.WriteLine("\nСотрудник с таким именем отсутствует");
                return;
            }
            
            if (nm is not null )
            {
                decimal tPay = 0;
                var sumHours = 0;
                string name = nm.FirstOrDefault(s => s.Name.ToLower() == nameEmp.ToLower()).Name;

                Console.WriteLine($"Отчет по сотруднику: {name} за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()}");
                foreach (var item in nm)
                {
                    var totalPH = item.TimeRecords.Where(t => t.Date >= startDate);
                    foreach (var ph in totalPH)
                    {
                        tPay += ph.TotalPay;
                        sumHours += ph.Hours;
                        Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                        
                    }
                }
                Manager.TotalPay = tPay;
                Employee.TotalPay = tPay;
                Freelancer.TotalPay = tPay;
                Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                Console.WriteLine("___________________________\n");
            }
            else
            {
                Console.WriteLine($"В период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} сотрудник {nameEmp} не работал");
            }
        }

        public static void DisplayStats(string name = "", bool manager = false)
        {
            string nameEmp;
            if (manager)
            {
                Console.Write("Укажите имя сотрудника для построения отчета: ");
                nameEmp = Console.ReadLine();
                Console.WriteLine("\n");
                CalcStats(nameEmp);
            }

            nameEmp = name;
            if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                CalcStats(nameEmp);
            }
            else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                CalcStats(nameEmp);                
            }
        }

        public static void DisplayAllStats()
        {
            Stats();
            CalcStatsOfAll();
        }
    }
}
