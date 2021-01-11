using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.IO;
using System.Threading;

namespace CalcSalary
{
    //В соответствии с ТЗ отсюда - https://docs.google.com/document/d/1kZz1ozAwNTVkIxWoyPYI_zTw6mos3CI03MyXnCxNbeM/edit# 
    class Program
    {
        static void Main(string[] args)
        {
            Person.ActionMenu();
            //AllEmployeesHoursWorkedList.RemoveEmployee("tm");
            //AllEmployeesHoursWorkedList.RemoveEmployee("te");
            //AllEmployeesHoursWorkedList.RemoveEmployee("tf");
            //AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Manager, "Tm1", DateTime.Now.AddDays(-10), 1);
        }
    }
}