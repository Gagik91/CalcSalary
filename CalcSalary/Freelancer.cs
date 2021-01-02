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
        
    }
}

