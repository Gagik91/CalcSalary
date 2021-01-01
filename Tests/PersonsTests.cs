using NUnit.Framework;
using System;
using CalcSalary;
using System.Collections.Generic;

namespace Tests
{
    //public class PersonsTests
    //{
    //    [SetUp]
    //    public void Setup()
    //    {
    //    }

    //    [Test]
    //    public void ManagerTotalPay()
    //    {

    //        Manager manager = new Manager("tests", new List<TimeRecord>()
    //        {
    //            new TimeRecord(DateTime.Now.AddDays(-3), "test", 8, "test message"),
    //            new TimeRecord(DateTime.Now.AddDays(-2), "test", 9, "test message"),
    //            new TimeRecord(DateTime.Now.AddDays(-1), "test", 7, "test message"),
    //        });


    //        //10000 + 11000 + 8 750 = 29750
    //        Assert.IsTrue(manager.TotalPay == 29750);
    //    }
    //    [Test]
    //    public void EmployeeTotalPay()
    //    {

    //        Employee employee = new Employee("tests", new List<TimeRecord>()
    //        {
    //            new TimeRecord(DateTime.Now.AddDays(-3), "test", 8, "test message"),
    //            new TimeRecord(DateTime.Now.AddDays(-2), "test", 9, "test message"),
    //            new TimeRecord(DateTime.Now.AddDays(-1), "test", 7, "test message"),
    //        });



    //        //6000 + 7000 + 5250
    //        Assert.IsTrue(employee.TotalPay == 18250);
    //    }
    //    [Test]
    //    public void FreelancerTotalPay()
    //    {

    //        Freelancer freelancer = new Freelancer("tests", new List<TimeRecord>()
    //        {
    //            new TimeRecord(DateTime.Now.AddDays(-3), "test", 8, "test message"),
    //            new TimeRecord(DateTime.Now.AddDays(-2), "test", 9, "test message"),
    //            new TimeRecord(DateTime.Now.AddDays(-1), "test", 7, "test message"),
    //        });


    //        //8000 + 9000 + 7000 = 24000
    //        Assert.IsTrue(freelancer.TotalPay == 24000);
    //    }

    //}
}