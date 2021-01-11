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
        private static byte period;
        private static DateTime today = DateTime.Today;
        private static decimal TotalPay { get; set; }

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

            
            using (ContextWithDB db = new ContextWithDB())
            {
                decimal tPay = 0;   //Общая плата за указанный период для конкретного сотрудника
                int sumHours = 0;   //Общие часы отработанные сотрудником за указанный период

                //Получаем данные из БД и выводим их
                var emp = db.AllEmployeesHoursWorkedList.OrderBy(n => n.Name).ToList();
                for (int i = 0; i < emp.Count; i++)
                {
                    if (i == emp.Count - 1)
                    {
                        sumHours += emp[i].WorkedHours;
                        tPay = ActionsOfEmployees.SalaryCalc((byte)sumHours, emp[i].RoleEmployee);
                        Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{emp[i].Name} отработал {sumHours} часов и заработал за период {tPay}");
                        Console.WriteLine("________");
                        hoursWorked += sumHours;
                        amountToPaid += tPay;
                        sumHours = 0;
                        tPay = 0;
                        break;
                    }
                    else if (emp[i].Name.ToLower() == emp[i + 1].Name.ToLower())
                    {
                        sumHours += emp[i].WorkedHours;
                    }
                    else
                    {
                        sumHours += emp[i].WorkedHours;
                        tPay = ActionsOfEmployees.SalaryCalc((byte)sumHours, emp[i].RoleEmployee);
                        Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{emp[i].Name} отработал {sumHours} часов и заработал за период {tPay}");
                        Console.WriteLine("________");
                        hoursWorked += sumHours;
                        amountToPaid += tPay;
                        sumHours = 0;
                        tPay = 0;
                    }
                }
                Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} \n");
            }
        }

        //расчет статистики конкретного сотрудника по имени
        public static void CalcStats(string nameEmp, byte per = 0, Settings.Roles? role = null)
        {
            using (ContextWithDB db = new ContextWithDB())
            {
                if (db.AllEmployeesHoursWorkedList.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
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

                    var n = db.AllEmployeesHoursWorkedList.Where(n => n.Name.ToLower() == nameEmp.ToLower()).ToList();
                    if (n is not null)
                    {

                        decimal tPay = 0;   //Общая плата за указанный период для конкретного сотрудника
                        int sumHours = 0;   //Общие часы отработанные сотрудником за указанный период

                        Console.WriteLine($"Отчет по сотруднику: {nameEmp} за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()}");
                        var emp = db.AllEmployeesHoursWorkedList.Where(n => n.Name == nameEmp).Where(d => d.Date >= startDate).ToList();

                        for (int i = 0; i < emp.Count; i++)
                        {
                            tPay += ActionsOfEmployees.SalaryCalc((byte)emp[i].WorkedHours, emp[i].RoleEmployee);
                            sumHours += emp[i].WorkedHours;
                            Console.WriteLine($"{emp[i].Date.ToShortDateString()}, {emp[i].WorkedHours} часов, {emp[i].Message}");
                        }
                        TotalPay = tPay;
                        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {TotalPay}");
                        Console.WriteLine("___________________________\n");
                    }
                }
                
                else
                {
                    Console.WriteLine($"В компании не работает сотрудник {nameEmp}");
                }
            }
        }
        
        public static void DisplayStats(string name = "")
        {
            using (ContextWithDB db = new ContextWithDB())
            {
                string nameEmp;
                Settings.Roles empRole = db.AllEmployeesHoursWorkedList.FirstOrDefault(n => n.Name.ToLower() == name.ToLower()).RoleEmployee;
                if (empRole == Settings.Roles.Manager)
                {
                    Console.Write("Укажите имя сотрудника для построения отчета: ");
                    nameEmp = Console.ReadLine();
                    Console.WriteLine("\n");
                    CalcStats(nameEmp, role: empRole);
                }
                else
                {
                    nameEmp = name;
                    CalcStats(nameEmp, role: empRole);
                }
            }
        }
        public static void DisplayAllStats()
        {
            Stats();
            CalcStatsOfAll();
        }
    }
}

