using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.IO;
using System.Threading;

namespace CalcSalary
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите Ваше имя: ");
            string name = Console.ReadLine();
            Manager.AddEmployee("Oganisyan", 2, 1);
            Manager.AddEmployee("OganisyanZZZZZ", 12, 1);
            Manager.AddEmployee("Gagik", 2, 1);
            Manager.AddEmployee("Silva", 5, 1);
            Manager.AddEmployee("Garik", 2, 2);
            Manager.AddEmployee("Gohar", 3, 3);
            Manager.AddEmployee("Hripsime", 3, 3);
            TimeRecord.FilesActionManager();
            TimeRecord.FilesActionEmployee();
            TimeRecord.FilesActionFreelancer();

            if (Manager.manager.Any(n => n.Name.ToLower() == name.ToLower()))
            {
                Manager.PrintHello(name);
            }
            else if (Employee.employee.Any(n => n.Name.ToLower() == name.ToLower()))
            {
                Employee.PrintHello(name);
            }
            else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == name.ToLower()))
            {
                Employee.PrintHello(name);
            }

            else
            {
                Console.WriteLine($"Сотрудник с именем {name} не найден");
            }

            Console.ReadLine();
        }
    }
}
