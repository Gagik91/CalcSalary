using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.IO;
using System.Threading;

namespace CalcSalary
{
    public class TimeRecord
    {
        public DateTime Date{ get; set; }
        public string Name{ get; set; }
        public byte Hours { get; set; }
        public string Message { get; set; }
        public decimal TotalPay { get; set; }

        public static string manFile = @"F:\CalcSalary\managerHoursWorkedList.csv";
        public static string empFile = @"F:\CalcSalary\employeeHoursWorkedList.csv";
        public static string freeFile = @"F:\CalcSalary\freelancerHoursWorkedList.csv";
        public static string employeeListFile = @"F:\CalcSalary\employeeListFile.csv";

        public static void FilesActionManager()
        {
            //получаем массив string в каждом элементе следующая информация:
            //дата, имя, отработанных количество часов, сообщение
            var manData = ManagerReader().Split('\n', int.MaxValue);

            List<string> manName = new List<string>();
            List<string> manDate = new List<string>();
            List<byte> manHours = new List<byte>();
            List<string> manMessage = new List<string>();

            foreach (var item in manData)
            {
                int startName = 0;
                int endName = 0;
                int startHours = 0;
                int endHours = 0;
                int startMessage = 0;

                if (item.Length > 0)
                {
                    manDate.Add(item[0..10]);
                    startName = item.IndexOf(", ") + 2;
                    string tempName = item.Substring(startName);
                    endName = tempName.IndexOf(", ");
                    manName.Add(tempName.Substring(0, endName));

                    startHours = endName + 1;
                    string tempHours = tempName.Substring(startHours);
                    endHours = tempHours.IndexOf(", ");
                    manHours.Add(byte.Parse(tempHours.Substring(0, endHours)));

                    startMessage = endHours + 2;
                    manMessage.Add(tempHours.Substring(startMessage));
                }
            }

            for (int i = 0; i < manName.Count; i++)
            {
                ActionsOfEmployees.AddMan(manName[i], manHours[i], DateTime.Parse(manDate[i]), manMessage[i]);
            }
        }

        public static void FilesActionEmployee()
        {
            //получаем массив string в каждом элементе следующая информация:
            //дата, имя, отработанных количество часов, сообщение
            var empData = EmployeeReader().Split('\n', int.MaxValue);

            List<string> empName = new List<string>();
            List<string> empDate = new List<string>();
            List<byte> empHours = new List<byte>();
            List<string> empMessage = new List<string>();

            foreach (var item in empData)
            {
                int startName = 0;
                int endName = 0;
                int startHours = 0;
                int endHours = 0;
                int startMessage = 0;

                if (item.Length > 0)
                {
                    empDate.Add(item[0..10]);
                    startName = item.IndexOf(",") + 1;
                    string tempName = item.Substring(startName);
                    endName = tempName.IndexOf(", ");
                    empName.Add(tempName.Substring(0, endName));

                    startHours = endName + 1;
                    string tempHours = tempName.Substring(startHours);
                    endHours = tempHours.IndexOf(", ");
                    empHours.Add(byte.Parse(tempHours.Substring(0, endHours)));

                    startMessage = endHours + 2;
                    empMessage.Add(tempHours.Substring(startMessage));
                }
            }

            for (int i = 0; i < empName.Count; i++)
            {
                ActionsOfEmployees.AddEmp(empName[i], empHours[i], DateTime.Parse(empDate[i]), empMessage[i]);
            }
        }
        public static void FilesActionFreelancer()
        {
            //получаем массив string в каждом элементе следующая информация:
            //дата, имя, отработанных количество часов, сообщение
            var freeData = FreelancerReader().Split('\n', int.MaxValue);

            List<string> freeName = new List<string>();
            List<string> freeDate = new List<string>();
            List<byte> freeHours = new List<byte>();
            List<string> freeMessage = new List<string>();

            foreach (var item in freeData)
            {
                int startName = 0;
                int endName = 0;
                int startHours = 0;
                int endHours = 0;
                int startMessage = 0;

                if (item.Length > 0)
                {
                    freeDate.Add(item[0..10]);
                    startName = item.IndexOf(",") + 1;
                    string tempName = item.Substring(startName);
                    endName = tempName.IndexOf(", ");
                    freeName.Add(tempName.Substring(0, endName));

                    startHours = endName + 1;
                    string tempHours = tempName.Substring(startHours);
                    endHours = tempHours.IndexOf(", ");
                    freeHours.Add(byte.Parse(tempHours.Substring(0, endHours)));

                    startMessage = endHours + 2;
                    freeMessage.Add(tempHours.Substring(startMessage));
                }
            }

            for (int i = 0; i < freeName.Count; i++)
            {
                ActionsOfEmployees.AddFree(freeName[i], freeHours[i], DateTime.Parse(freeDate[i]), freeMessage[i]);
            }
        }


        public static string ManagerReader()
        {
            using (StreamReader employeeListStreamReader = new StreamReader(manFile, true))
            {
                string str = employeeListStreamReader.ReadToEnd();
                return str;
            }
        }
        public static string EmployeeReader()
        {
            using (StreamReader employeeListStreamReader = new StreamReader(empFile, true))
            {
                string str = employeeListStreamReader.ReadToEnd();
                return str;
            }
        }
        public static string FreelancerReader()
        {
            using (StreamReader employeeListStreamReader = new StreamReader(freeFile, true))
            {
                string str = employeeListStreamReader.ReadToEnd();
                return str;
            }
        }

        public static void ManagerList (DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(manFile, true))
            {
                employeeListStreamWriter.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }

        public static void EmployeeList(DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(empFile, true))
            {
                employeeListStreamWriter.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }
        public static void FreelancerList(DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(freeFile, true))
            {
                employeeListStreamWriter.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }
        public void EmpList(string name, string role)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(employeeListFile, true))
            {
                employeeListStreamWriter.WriteLine($"{Name}, {role}");
            }
        }
        public TimeRecord(DateTime date, string name, byte hours, string message, decimal tPay, string role, bool newAdd = false)
        {
            Date = date;
            Name = name;
            Hours = hours;
            Message = message;
            TotalPay = tPay;

            if (newAdd)
            {
                if (role == "manager")
                {
                    ManagerList(Date, Name, Hours, Message);
                    ManagerReader();
                }
                else if (role == "employee")
                {
                    EmployeeList(Date, Name, Hours, Message);
                    EmployeeReader();
                }
                else if (role == "freelancer")
                {
                    FreelancerList(Date, Name, Hours, Message);
                    FreelancerReader();
                }
                EmpList(Name, role);
            }
        }
    }
}
