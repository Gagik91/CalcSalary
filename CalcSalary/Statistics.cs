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
        public static decimal TotalPay = 0;
        public static string whatWorkWas;

        public static void AddPay(List<TimeRecord> timeRecords, byte hr = 0)
        {
            decimal payPerHour = Settings.Manager.MonthSalary / Settings.WorkHoursInMonth;
            decimal totalPay = 0;
            decimal bonusPerDay = (Settings.Manager.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;

            foreach (var timeRecord in timeRecords)
            {
                if (timeRecord.Hours <= Settings.WorkHourInDay)
                {
                    totalPay += timeRecord.Hours * payPerHour;
                    timeRecord.TotalPay += totalPay;
                }
                else // переработка
                {
                    totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
                    timeRecord.TotalPay += totalPay;
                }
            }
        }
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


        public static void AddHours(string name = "", bool manager = false)
        {            
            string nameEmp;
            if (manager)
            {                
                Console.WriteLine("Укажите имя сотрудника для добавления часов работы: ");
                nameEmp = Console.ReadLine();
                if (Manager.manager.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    nameEmp = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).FirstOrDefault().Name;
                    Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020): ");
                    TimeSpan day = today - DateTime.Parse(Console.ReadLine());

                    Console.WriteLine($"Введите количество часов для добавления сотруднику {nameEmp}: ");
                    byte hr = byte.Parse(Console.ReadLine());
                    
                    Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                    whatWorkWas = Console.ReadLine();
                    foreach (var item in Manager.manager)
                    {
                        if (item.Name.ToLower() == nameEmp.ToLower())
                        {
                            TotalPay = SalaryCalcManager(hr);
                            item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "manager"));
                        }
                    }
                    DisplayStats(name,true);
                }

                else if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    nameEmp = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).FirstOrDefault().Name;
                    Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020): ");
                    TimeSpan day = today - DateTime.Parse(Console.ReadLine());

                    Console.WriteLine($"Введите количество часов для добавления сотруднику {nameEmp}: ");
                    byte hr = byte.Parse(Console.ReadLine());

                    Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                    whatWorkWas = Console.ReadLine();
                    foreach (var item in Employee.employee)
                    {
                        if (item.Name.ToLower() == nameEmp.ToLower())
                        {
                            TotalPay = SalaryCalcEmployee(hr);
                            item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "employee"));
                        }
                    }
                    DisplayStats(name,true);
                }

                else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    nameEmp = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).FirstOrDefault().Name;
                    Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020): ");
                    TimeSpan day = today - DateTime.Parse(Console.ReadLine());

                    Console.WriteLine($"Введите количество часов для добавления сотруднику {nameEmp}: ");
                    byte hr = byte.Parse(Console.ReadLine());

                    Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                    whatWorkWas = Console.ReadLine();
                    foreach (var item in Freelancer.freelancer)
                    {
                        if (item.Name.ToLower() == nameEmp.ToLower())
                        {
                            TotalPay = SalaryCalcFreelancer(hr);
                            item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "freelancer"));
                        }
                    }
                    DisplayStats(name, true);
                }
            }
            nameEmp = name;
            if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                Console.WriteLine("Укажите в какой день добавить часы (шаблон: 01.01.2020): ");
                TimeSpan day = today - DateTime.Parse(Console.ReadLine());

                Console.WriteLine($"Введите количество часов для добавления: ");
                byte hr = byte.Parse(Console.ReadLine());

                Console.WriteLine($"Укажите какую работу выполняли: ");
                whatWorkWas = Console.ReadLine();
                foreach (var item in Employee.employee)
                {
                    if (item.Name.ToLower() == nameEmp.ToLower())
                    {
                        TotalPay = SalaryCalcEmployee(hr);
                        item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "employee"));
                    }
                }
                DisplayStats(name,false);
            }
            else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                Console.WriteLine($"Укажите в какой день добавить часы (шаблон: 01.01.2020) самое ранее за {DateTime.Now.AddDays(-2).ToShortDateString()}: ");
                TimeSpan day = today - DateTime.Parse(Console.ReadLine());
                if (day.Days <= 2)
                {
                    Console.WriteLine($"Введите количество часов для добавления {nameEmp}");
                    byte hr = byte.Parse(Console.ReadLine());
                    var nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower());
                    Console.WriteLine($"Укажите какую работу выполнял {nameEmp}");
                    whatWorkWas = Console.ReadLine();
                    foreach (var item in Freelancer.freelancer)
                    {
                        if (item.Name.ToLower() == nameEmp.ToLower())
                        {
                            TotalPay = SalaryCalcFreelancer(hr);
                            item.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-day.Days), nameEmp, hr, whatWorkWas, TotalPay, "freelancer"));
                        }
                    }
                    DisplayStats(name, false);
                }
                else
                {
                    Console.WriteLine($"Самая рання дата может быть {DateTime.Now.AddDays(-2).ToShortDateString()}");
                }
            }
        }

        public static void DisplayAllStats()
        {
            Stats();

            int hoursWorked = 0;
            decimal amountToPaid = 0;

            if (period == 1)
            {
                foreach (var item in Manager.manager)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours; 
                    }
                    Console.WriteLine($"Отчет за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                }
                foreach (var item in Employee.employee)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                foreach (var item in Freelancer.freelancer)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} ");
            }
            else if (period == 2)
            {
                foreach (var item in Manager.manager)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                foreach (var item in Employee.employee)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                foreach (var item in Freelancer.freelancer)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} ");
            }
            else if (period == 3)
            {
                foreach (var item in Manager.manager)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-31).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                foreach (var item in Employee.employee)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-31).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                foreach (var item in Freelancer.freelancer)
                {
                    var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-31).Day);
                    decimal tPay = 0;
                    var sumHours = 0;
                    foreach (var h in tRecords)
                    {
                        tPay += item.TotalPay;
                        sumHours += h.Hours;
                    }
                    hoursWorked += sumHours;
                    amountToPaid += tPay;
                    Console.WriteLine($"Отчет за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()} день \n{item.Name} отработал {sumHours} часов и заработал за период {tPay}");
                    Console.WriteLine("________");
                }
                Console.WriteLine($"Всего часов отработано за период {hoursWorked}, сумма к выплате {amountToPaid} ");
            }
        }


       


        public static void Stats()
        {
            Console.WriteLine("Выберите период за который требуется просмотреть отчет");
            Console.WriteLine("Нажмите 1, чтобы просмотреть отчет за 1 день");
            Console.WriteLine("Нажмите 2, чтобы просмотреть отчет за неделю");
            Console.WriteLine("Нажмите 3, чтобы просмотреть отчет за месяц");
            period = byte.Parse(Console.ReadLine());

        }


        public static void DisplayStats(string name = "", bool manager = false)
        {
            string nameEmp;
            if (manager)
            {

                Console.WriteLine("Укажите имя сотрудника для построения отчета: ");
                nameEmp = Console.ReadLine();

                if (Manager.manager.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    Stats();
                    if (period == 1)
                    {
                        var nm = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-1)));
                        foreach (var item in nm)
                        {
                            var totalPH = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                            decimal tPay = 0;
                            var sumHours = 0;
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }
                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }
                    else if (period == 2)
                    {
                        var nm = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-7)));
                        foreach (var item in nm)
                        {
                            var totalPH = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                            decimal tPay = 0;
                            var sumHours = 0;
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }

                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }
                    else if (period == 3)
                    {
                        var nm = Manager.manager.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-31)));
                        foreach (var item in nm)
                        {
                            decimal tPay = 0;
                            var sumHours = 0;

                            var totalPH = item.TimeRecords.Where(n => n.Date >= today.AddDays(-31));
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }

                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }


                }
                else if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    Stats();
                    if (period == 1)
                    {
                        var nm = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-1)));
                        foreach (var item in nm)
                        {
                            var totalPH = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                            decimal tPay = 0;
                            var sumHours = 0;
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }
                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }

                    else if (period == 2)
                    {
                        var nm = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-7)));
                        foreach (var item in nm)
                        {
                            var totalPH = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                            decimal tPay = 0;
                            var sumHours = 0;
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }

                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }
                    else if (period == 3)
                    {
                        var nm = Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-31)));
                        foreach (var item in nm)
                        {
                            decimal tPay = 0;
                            var sumHours = 0;

                            var totalPH = item.TimeRecords.Where(n => n.Date >= today.AddDays(-31));
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }

                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }


                }

                else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()))
                {
                    Stats();
                    if (period == 1)
                    {
                        var nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-1)));
                        foreach (var item in nm)
                        {
                            var totalPH = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                            decimal tPay = 0;
                            var sumHours = 0;
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }
                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }
                    else if (period == 2)
                    {
                        var nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-7)));
                        foreach (var item in nm)
                        {
                            var totalPH = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                            decimal tPay = 0;
                            var sumHours = 0;
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }

                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }
                    else if (period == 3)
                    {
                        var nm = Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()).Where(n => n.TimeRecords.Any(n => n.Date >= today.AddDays(-31)));
                        foreach (var item in nm)
                        {
                            decimal tPay = 0;
                            var sumHours = 0;

                            var totalPH = item.TimeRecords.Where(n => n.Date >= today.AddDays(-31));
                            foreach (var ph in totalPH)
                            {
                                tPay += ph.TotalPay;
                                sumHours += ph.Hours;
                                Console.WriteLine($"{ph.Date.ToShortDateString()}, {ph.Hours} часов, {ph.Message}");
                            }

                            Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                            Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                            Console.WriteLine("___________________________");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Сотрудник с таким именем отсутствует");
                }
            }

            nameEmp = name;
            if (Employee.employee.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                Stats();
                if (period == 1)
                {
                    var nm = (Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()));
                    foreach (var item in nm)
                    {
                        var tRecords = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                        decimal tPay = 0;
                        var sumHours = 0;
                        foreach (var h in tRecords)
                        {
                            tPay += item.TotalPay;
                            sumHours += h.Hours;
                            Console.WriteLine($"{h.Date.ToShortDateString()}, {h.Hours} часов, {h.Message}");
                        }
                        Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                        Console.WriteLine("___________________________");
                    }
                }
                else if (period == 2)
                {
                    var nm = (Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()));
                    foreach (var item in nm)
                    {
                        var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                        decimal tPay = 0;
                        var sumHours = 0;
                        foreach (var h in tRecords)
                        {
                            tPay += item.TotalPay;
                            sumHours += h.Hours;
                            Console.WriteLine($"{h.Date.ToShortDateString()}, {h.Hours} часов, {h.Message}");
                        }

                        Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                        Console.WriteLine("___________________________");
                    }
                }
                else if (period == 3)
                {
                    var nm = (Employee.employee.Where(n => n.Name.ToLower() == nameEmp.ToLower()));
                    foreach (var item in nm)
                    {
                        var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-31).Day);
                        decimal tPay = 0;
                        var sumHours = 0;
                        foreach (var h in tRecords)
                        {
                            tPay += item.TotalPay;
                            sumHours += h.Hours;
                            Console.WriteLine($"{h.Date.ToShortDateString()}, {h.Hours} часов, {h.Message}");
                        }

                        Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                        Console.WriteLine("___________________________");
                    }
                }


            }
            else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == nameEmp.ToLower()) && manager == false)
            {
                Stats();
                if (period == 1)
                {
                    var nm = (Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()));
                    foreach (var item in nm)
                    {
                        var tRecords = item.TimeRecords.Where(t => t.Date.Day == today.AddDays(-1).Day);
                        decimal tPay = 0;
                        var sumHours = 0;
                        foreach (var h in tRecords)
                        {
                            tPay += item.TotalPay;
                            sumHours += h.Hours;
                            Console.WriteLine($"{h.Date.ToShortDateString()}, {h.Hours} часов, {h.Message}");
                        }
                        Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-1).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                        Console.WriteLine("___________________________");
                    }
                }
                else if (period == 2)
                {
                    var nm = (Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()));
                    foreach (var item in nm)
                    {
                        var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-7).Day);
                        decimal tPay = 0;
                        var sumHours = 0;
                        foreach (var h in tRecords)
                        {
                            tPay += item.TotalPay;
                            sumHours += h.Hours;
                            Console.WriteLine($"{h.Date.ToShortDateString()}, {h.Hours} часов, {h.Message}");
                        }

                        Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-7).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                        Console.WriteLine("___________________________");
                    }
                }
                else if (period == 3)
                {
                    var nm = (Freelancer.freelancer.Where(n => n.Name.ToLower() == nameEmp.ToLower()));
                    foreach (var item in nm)
                    {
                        var tRecords = item.TimeRecords.Where(t => t.Date.Day >= today.AddDays(-31).Day);
                        decimal tPay = 0;
                        var sumHours = 0;
                        foreach (var h in tRecords)
                        {
                            tPay += item.TotalPay;
                            sumHours += h.Hours;
                            Console.WriteLine($"{h.Date.ToShortDateString()}, {h.Hours} часов, {h.Message}");
                        }

                        Console.WriteLine($"Отчет по сотруднику: {item.Name} за период с {today.AddDays(-31).ToShortDateString()} по {today.AddDays(0).ToShortDateString()}");
                        Console.WriteLine($"Итого: отработанные часы - {sumHours}, заработано: {tPay}");
                        Console.WriteLine("___________________________");
                    }
                }
            }
        }
    }
}
