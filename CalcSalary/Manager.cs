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
            ActionsOfEmployees.AddPay(timeRecords);           
        }

        //public void AddPay(List<TimeRecord> timeRecords, byte hr = 0)
        //{
        //    decimal payPerHour = Settings.Manager.MonthSalary / Settings.WorkHoursInMonth;
        //    decimal totalPay = 0;
        //    decimal bonusPerDay = (Settings.Manager.MonthBonus / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;

        //    foreach (var timeRecord in timeRecords)
        //    {
        //        if (timeRecord.Hours <= Settings.WorkHourInDay)
        //        {
        //            totalPay += timeRecord.Hours * payPerHour;
        //        }
        //        else // переработка
        //        {
        //            totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
        //        }
        //    }
        //    TotalPay += totalPay;
        //}

        //public static void AddManager(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        //{
        //    ActionsOfEmployees.SalaryCalcManager(hours);
        //    if (dt is null)
        //    {
        //        dt = DateTime.Now;
        //    }
        //    Manager m = new Manager(name, new List<TimeRecord>()
        //    {
        //            new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", ActionsOfEmployees.TotalPay, "manager", newMan)
        //    });
        //    manager.Add(m);
        //}
        //public static void AddEmployee(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        //{
        //    ActionsOfEmployees.SalaryCalcEmployee(hours);
        //    if (dt is null)
        //    {
        //        dt = DateTime.Now;
        //    }
        //    Employee e = new Employee(name, new List<TimeRecord>()
        //    { 
        //        new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", ActionsOfEmployees.TotalPay, "employee", newMan)
        //    });
        //    Employee.employee.Add(e);
        //}
        //public static void AddFreeLancer(string name, byte hours, DateTime? dt = null, string message = "", bool newMan = false)
        //{
        //    ActionsOfEmployees.SalaryCalcFreelancer(hours);
        //    if (dt is null)
        //    {
        //        dt = DateTime.Now;
        //    }
        //    Freelancer f = new Freelancer(name, new List<TimeRecord>()
        //    {
        //        new TimeRecord(dt.Value.AddDays(0), name, hours, "Start work", ActionsOfEmployees.TotalPay, "freelancer", newMan)
        //    });
        //    Freelancer.freelancer.Add(f);
        //}
    }
}
