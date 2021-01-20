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
        private byte period;
        private static DateTime today = DateTime.Today;

        //метод выбора периода
        public void Stats()
        {
            period = 0;
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
        }
        //public static void CalcStatsOfAll()
        //{
        //    Person p = new Person();
        //    DateTime startDate = new DateTime();

        //    int hoursWorked = 0;
        //    decimal amountToPaid = 0;

        //    if (period == 1)
        //    {
        //        startDate = today.AddDays(-1);                
        //    }
        //    else if (period == 2)
        //    {
        //        startDate = today.AddDays(-7);
        //    }
        //    else if (period == 3)
        //    {
        //        startDate = today.AddMonths(-1);
        //    }

        //    foreach (var item in Manager.manager)
        //    {
        //        var tRecords = item.TimeRecords.Where(t => t.Date >= startDate);
        //        decimal tPay = 0;
        //        int sumHours = 0;
        //        foreach (var h in tRecords)
        //        {
        //            tPay += h.TotalPay;
        //            sumHours += h.Hours;
        //        }
        //        Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
        //        Console.WriteLine("________");
        //        hoursWorked += sumHours;
        //        amountToPaid += tPay;
        //    }
        //    foreach (var item in Employee.employee)
        //    {
        //        var tRecords = item.TimeRecords.Where(t => t.Date >= startDate);
        //        decimal tPay = 0;
        //        var sumHours = 0;
        //        foreach (var h in tRecords)
        //        {
        //            tPay += h.TotalPay;
        //            sumHours += h.Hours;
        //        }
        //        Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
        //        Console.WriteLine("________");
        //        hoursWorked += sumHours;
        //        amountToPaid += tPay;

        //    }
        //    foreach (var item in Freelancer.freelancer)
        //    {
        //        var tRecords = item.TimeRecords.Where(t => t.Date >= startDate);
        //        decimal tPay = 0;
        //        var sumHours = 0;
        //        foreach (var h in tRecords)
        //        {
        //            tPay += h.TotalPay;
        //            sumHours += h.Hours;
        //        }
        //        Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
        //        Console.WriteLine("________");
        //        hoursWorked += sumHours;
        //        amountToPaid += tPay;
        //    }
        //    p.TotalPay += amountToPaid;
        //    Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} \n");
        //}

        //расчет статистики конкретного сотрудника по имени
        //public void CalcStats(string nameEmp)
        //{
        //    IEnumerable<Person> nm = null;
        //    DateTime startDate = new DateTime();
        //    Stats();
        //    if (period == 1)
        //    {
        //        startDate = today.AddDays(-1);
        //    }
        //    else if (period == 2)
        //    {
        //        startDate = today.AddDays(-7);
        //    }
        //    else if (period == 3)
        //    {
        //        startDate = today.AddMonths(-1);
        //    }

        //    if (Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
        //    {
        //        nm = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
        //    }
        //    else if (Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
        //    {
        //        nm = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
        //    }
        //    else if (Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Any(n => n.TimeRecords.Any(n => n.Date >= startDate)))
        //    {
        //        nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= startDate));
        //    }
        //    else
        //    {
        //        Console.WriteLine($"\nСотрудник {nameEmp} не работал в период с {startDate.Date.ToShortDateString()} по {today.Date.ToShortDateString()}");
        //        return;
        //    }
            
        //    if (nm is not null )
        //    {
        //        decimal tPay = 0;
        //        var sumHours = 0;
        //        string name = nm.FirstOrDefault(s => s.Name.ToLower() == nameEmp.ToLower()).Name;

        //        Console.WriteLine($"Отчет по сотруднику: {name} за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()}");
        //        foreach (var item in nm)
        //        {
        //            var totalPH = item.TimeRecords.Where(t => t.Date >= startDate);
        //            foreach (var ph in totalPH)
        //            {
        //                tPay += ph.TotalPay;
        //                sumHours += ph.Hours;
        //                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                        
        //            }
        //        }
                
        //        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
        //        Console.WriteLine("___________________________\n");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"В период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} сотрудник {nameEmp} не работал");
        //    }
        //}

        public void DisplayStats(string name, Settings.Role role)
        {
            Person p = new Person();
            Files f = new Files();
            List<(DateTime dT, string name, byte hours, decimal tPay, string message)> tupleData = new List<(DateTime dT, string name, byte hours, decimal tPay, string message)>();
            Stats();
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
            Person p = new Person();
            Stats();
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
            List<(DateTime dT, string name, byte hours, decimal tPay, string message)> tupleData = new List<(DateTime dT, string name, byte hours, decimal tPay, string message)>();
            tupleData = f.FilesAction(Files.manFile, Files.empFile, Files.freeFile).Where(n => n.dT >= startDate).ToList();
            int hoursWorked = 0;
            decimal amountToPaid = 0;
            
            
            for (int i = 0; i < tupleData.Count; i++)
            {                
                Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {today.AddDays(-1).ToShortDateString()} день \n{tupleData[i].name} отработал {tupleData[i].hours} часов и заработал за период {tupleData[i].tPay}");
                Console.WriteLine("________");
                amountToPaid += tupleData[i].tPay;
                hoursWorked += tupleData[i].hours;
            }

            p.TotalPay += amountToPaid;
            Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} \n");
            
        }
        public int CountOfEmployee()
        {
            int count = 0;
            int col = 2;
            string personNames = "";
            using (TextFieldParser parser = new TextFieldParser(Files.employeeListFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (!personNames.Contains(fields[0]))
                    {
                        personNames += fields[0] + "\n";
                        count += fields.Length;
                    }
                }
            }
            return count / col;
        }
    }
}
