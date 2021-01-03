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
            Files.FilesActionManager();
            Files.FilesActionEmployee();
            Files.FilesActionFreelancer();
            Person.ActionMenu(name);
            Console.ReadLine();
        }
    }
}
