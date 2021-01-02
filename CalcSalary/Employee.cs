﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Employee : Staff
    {
        public static List<Employee> employee = new List<Employee>();
        public decimal TotalPay { get; set; }
        public Employee(string name, List<TimeRecord> timeRecords) : base(name, Settings.Employee.MonthSalary, timeRecords)
        {
            AddPay(timeRecords);
        }

        public void AddPay(List<TimeRecord> timeRecords, byte hr = 0)
        {
            decimal payPerHour = Settings.Employee.MonthSalary / Settings.WorkHoursInMonth;
            decimal totalPay = 0;
            decimal bonusPerDay = (Settings.Employee.MonthSalary / Settings.WorkHoursInMonth) * Settings.WorkHourInDay;

            foreach (var timeRecord in timeRecords)
            {
                if (timeRecord.Hours <= Settings.WorkHourInDay)
                {
                    totalPay += timeRecord.Hours * payPerHour;
                }
                else // переработка
                {
                    totalPay += (Settings.WorkHourInDay * payPerHour) + bonusPerDay;
                }
            }
            TotalPay += totalPay;
        }

       
    }
}

