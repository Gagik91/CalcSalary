using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic.FileIO;

namespace CalcSalary
{
    class Statistics
    {
        private DateTime today = DateTime.Today;
        public byte Stats()
        {
            byte period = 0;
            Console.WriteLine("Выберите период за который требуется просмотреть отчет");
            Console.WriteLine("Нажмите 1, чтобы просмотреть отчет за 1 день");
            Console.WriteLine("Нажмите 2, чтобы просмотреть отчет за неделю");
            Console.WriteLine("Нажмите 3, чтобы просмотреть отчет за месяц");
            
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
            return period;
        }
        public void DisplayStats(string name, Settings.Role role)
        {
            Files f = new Files();
            List<(DateTime dT, string name, byte hours, decimal tPay, string message)> tupleData = new List<(DateTime dT, string name, byte hours, decimal tPay, string message)>();
            byte period = Stats();
            DateTime startDate = new DateTime();

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
                        
            string nameEmp = "";

            if (role == Settings.Role.Manager)
            {
                Console.Write("Укажите имя сотрудника для построения отчета: ");
                nameEmp = Console.ReadLine();
                Console.WriteLine("\n");
                tupleData = f.FilesAction(Files.manFile, Files.empFile, Files.freeFile).Where(n => n.dT >= startDate && n.name.ToLower() == nameEmp.ToLower()).ToList();                 
            }
                        
            else if (role == Settings.Role.Employee)
            {
                nameEmp = name;
                tupleData = f.FilesAction(Files.empFile).Where(n => n.dT >= startDate && n.name.ToLower() == nameEmp.ToLower()).ToList();
            }
            else if (role == Settings.Role.Freelancer)
            {
                nameEmp = name;
                tupleData = f.FilesAction(Files.freeFile).Where(n => n.dT >= startDate && n.name.ToLower() == nameEmp.ToLower()).ToList();
            }

            if (tupleData.Count > 0)
            {
                decimal tPay = 0;
                var sumHours = 0;
                name = tupleData.Select(n=>n.name).FirstOrDefault();

                Console.WriteLine($"Отчет по сотруднику: {name} за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()}");
                foreach (var item in tupleData)
                {
                    tPay += item.tPay;
                    sumHours += item.hours;
                    Console.WriteLine($"{item.dT.ToShortDateString()}, {item.hours} часов, {item.message}");
                }

                Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                Console.WriteLine("___________________________\n");
            }
            else
            {
                Console.WriteLine($"\nСотрудник {nameEmp} не работал в период с {startDate.Date.ToShortDateString()} по {today.Date.ToShortDateString()}");                
            }
        }

        public void DisplayAllStats()
        {
            byte period = Stats();
            Person p = new Person();
            DateTime startDate = new DateTime();
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
            
            Files f = new Files();
            var tupleData = f.FilesAction(Files.manFile, Files.empFile, Files.freeFile).
                Where(n => n.dT >= startDate).GroupBy(n => n.name).
                Select(t => new { name = t.Key, hours = t.
                Sum(t => t.hours), totalPay = t.
                Sum(t => t.pay) }).ToList();            

            for (int i = 0; i < tupleData.Count; i++)
            {
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{tupleData[i].name} отработал {tupleData[i].hours} часов и заработал за период {tupleData[i].totalPay}");
                Console.WriteLine("________");
            }

            Console.WriteLine($"За указанный период отработано {tupleData.Sum(s=>s.hours)} часов, сумма к выплате {tupleData.Sum(s => s.totalPay)} \n");
        }        
    }
}
