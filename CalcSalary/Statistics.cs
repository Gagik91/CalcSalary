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
                Console.Write("Нужно ввести цифры из предложенных вариантов: ");
                string temp = Console.ReadLine();
                byte.TryParse(temp, out period);
                Console.WriteLine("\n");
            }
            return period;
        }
        public void DisplayStats(string name, Settings.Role role)
        {
            Files files = new Files();
            List<Person> data = new List<Person>();
            //List<AllCurrentData> data = new List<AllCurrentData>();
            ActionsOfEmployees actionsOfEmployees = new ActionsOfEmployees();
            byte period = Stats();
            DateTime startDate = new DateTime();
            switch (period)
            {
                case 1:
                    { startDate = today.AddDays(-1); }
                    break;
                case 2:
                    { startDate = today.AddDays(-7); }
                    break;
                case 3:
                    { startDate = today.AddMonths(-1); }
                    break;
                default:
                    break;
            }
                        
            string nameEmp = "";
            
            switch (role)
            {
                case Settings.Role.Manager:
                    {
                        Console.Write("Укажите имя сотрудника для построения отчета: ");
                        nameEmp = Console.ReadLine();
                        Console.WriteLine("\n");
                        var filteredByDate = actionsOfEmployees.FilteredByDate(startDate);
                        data = actionsOfEmployees.FilteredByName(nameEmp, filteredByDate);
                    }
                    break;
                case Settings.Role.Employee:
                    {
                        nameEmp = name;
                        var filteredByDate = actionsOfEmployees.FilteredByDate(startDate);
                        data = actionsOfEmployees.FilteredByName(nameEmp, filteredByDate);
                    }
                    break;
                case Settings.Role.Freelancer:
                    {
                        nameEmp = name;
                        var filteredByDate = actionsOfEmployees.FilteredByDate(startDate);
                        data = actionsOfEmployees.FilteredByName(nameEmp, filteredByDate);
                    }
                    break;
                default:
                    break;
            }

            if (data.Count > 0)
            {
                decimal tPay = 0;
                var sumHours = 0;
                name = data.Select(n=>n.Name).FirstOrDefault();

                Console.WriteLine($"Отчет по сотруднику: {name} за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()}");
                foreach (var item in data)
                {
                    tPay += item.Pay;
                    sumHours += item.Hours;
                    Console.WriteLine($"{item.Date.ToShortDateString()}, {item.Hours} часов, {item.Message}");
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
            ActionsOfEmployees actionsOfEmployees = new ActionsOfEmployees();
            DateTime startDate = new DateTime();
            switch (period)
            {
                case 1:
                    { startDate = today.AddDays(-1); }
                    break;
                case 2:
                    { startDate = today.AddDays(-7); }
                    break;
                case 3:
                    { startDate = today.AddMonths(-1); }
                    break;
                default:
                    break;
            }
            
            Files files = new Files();

            var filteredByDate = actionsOfEmployees.FilteredByDate(startDate);
            var sum = actionsOfEmployees.HoursAndPaySummedByName(acceptedData: filteredByDate);

            for (int i = 0; i < sum.Count; i++)
            {
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{sum[i].Name} отработал {sum[i].Hours} часов и заработал за период {sum[i].Pay}");
                Console.WriteLine("________");
            }

            Console.WriteLine($"За указанный период отработано {sum.Sum(s => s.Hours)} часов, сумма к выплате {sum.Sum(s => s.Pay)} \n");
        }        
    }
}
