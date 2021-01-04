using NUnit.Framework;
using System;
using CalcSalary;
using System.Collections.Generic;

namespace Tests
{
    public class PersonsTests
    {
        [SetUp]
        public void Setup()
        {
        }
        //тест оплаты менеджера с выбором периода
        [Test]
        public void ManagerTotalPay()
        {
            ActionsOfEmployees.AddMan("test manager", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddMan("test manager", 1, dt: DateTime.Now.AddDays(-5), message: "test message");
           
            Statistics.CalcStats("test manager", 2);

            Assert.IsTrue(Manager.TotalPay == 2500);
            
        }
        //тест оплаты штатного сотрудника с выбором периода
        [Test]
        public void EmployeeTotalPay()
        {
            ActionsOfEmployees.AddEmp("test employee", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddEmp("test employee", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            
            Statistics.CalcStats("test employee", 2);
           
            Assert.IsTrue(Employee.TotalPay == 1500);
        }
        //тест оплаты фрилансера с выбором периода
        [Test]
        public void FreelancerTotalPay()
        {

            ActionsOfEmployees.AddFree("test freelancer", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddFree("test freelancer", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            
            Statistics.CalcStats("test freelancer", 2);
            
            Assert.IsTrue(Freelancer.TotalPay == 2000);

        }

    }
}