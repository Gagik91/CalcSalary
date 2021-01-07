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
            Files.FilesActionManager();
            Files.FilesActionEmployee();
            Files.FilesActionFreelancer();
            Person.ActionMenu();
            Console.ReadLine();
        }
    }
}