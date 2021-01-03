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

        public static void Stats()
        {
            Console.WriteLine("Выберите период за который требуется просмотреть отчет");
            Console.WriteLine("Нажмите 1, чтобы просмотреть отчет за 1 день");
            Console.WriteLine("Нажмите 2, чтобы просмотреть отчет за неделю");
            Console.WriteLine("Нажмите 3, чтобы просмотреть отчет за месяц");
            period = 0;

            while (period < 1 || period > 3)
            {
                try
                {
                    Console.WriteLine("Нужно ввести цифры из предложенных вариантов");
                    period = byte.Parse(Console.ReadLine());
                }
                catch { continue; }
            }
        }
        public static void CalcStatsOfAll()
        {
            IEnumerable<Person> nm = null;
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
                startDate = today.AddDays(-31);
            }

            foreach (var item in Manager.manager)
            {
                var tRecords = item.TimeRecords.Where(t => t.Date >= startDate);
                decimal tPay = 0;
                var sumHours = 0;
                foreach (var h in tRecords)
                {
                    tPay += h.TotalPay;
                    sumHours += h.Hours;
                }
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
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
                hoursWorked += sumHours;
                amountToPaid += tPay;
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                Console.WriteLine("________");
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
                hoursWorked += sumHours;
                amountToPaid += tPay;
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                Console.WriteLine("________");
            }
            Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} ");
        }

        public static void CalcStats(string nameEmp)
        {
            IEnumerable<Person> nm = null;
            DateTime startDate = new DateTime();
            if (Manager.manager.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
            {
                Stats();
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
                    startDate = today.AddDays(-31);
                }

                if (Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
                {
                    nm = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
                }
            }

            else if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
            {
                Stats();
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
                    startDate = today.AddDays(-31);
                }

                if (Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
                {
                    nm = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
                }
            }
            else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
            {
                Stats();
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
                    startDate = today.AddDays(-31);
                }

                if (Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
                {
                    nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
                }
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

                Console.WriteLine($"\nОтчет по сотруднику: {name} за период с {startDate.ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
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
                Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                Console.WriteLine("___________________________");
            }
            else
            {
                Console.WriteLine($"В период с {startDate.ToShortDateString()} по {today.AddDays(0).ToShortDateString()} суотрудник {nameEmp} не работал");
            }
        }

        public static void DisplayStats(string name = "", bool manager = false)
        {
            string nameEmp;
            if (manager)
            {
                Console.WriteLine("\nУкажите имя сотрудника для построения отчета: ");
                nameEmp = Console.ReadLine();
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
