using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalcSalary
{
    public class Files
    {
        public const string manFile = @"..\..\..\Files\managerHoursWorkedList.csv";     //список всех менеджеров с отработанными часами
        public const string empFile = @"..\..\..\Files\employeeHoursWorkedList.csv";    //список всех сотрудников на зарплате с отработанными часами
        public const string freeFile = @"..\..\..\Files\freelancerHoursWorkedList.csv"; //список всех фрилансеров с отработанными часами
        public const string employeeListFile = @"..\..\..\Files\employeeListFile.csv";  //список всех сотрудников с указанием ролей


        public static void FilesActionManager()
        {            
            var manData = Reader(manFile).Split('\n');

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

                if (item.Length > 1)
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
                ActionsOfEmployees a = new ActionsOfEmployees();
                a.AddMan(manName[i], manHours[i], DateTime.Parse(manDate[i]), manMessage[i]);
            }
        }

        public static void FilesActionEmployee()
        {
            var empData = Reader(empFile).Split('\n');

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

                if (item.Length > 1)
                {
                    empDate.Add(item[0..10]);
                    startName = item.IndexOf(", ") + 2;
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
                ActionsOfEmployees a = new ActionsOfEmployees();
                a.AddEmp(empName[i], empHours[i], DateTime.Parse(empDate[i]), empMessage[i]);
            }
        }

        public static void FilesActionFreelancer()
        {            
            var freeData = Reader(freeFile).Split('\n');

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

                if (item.Length > 1)
                {
                    freeDate.Add(item[0..10]);
                    startName = item.IndexOf(", ") + 2;
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
                ActionsOfEmployees a = new ActionsOfEmployees();
                a.AddFree(freeName[i], freeHours[i], DateTime.Parse(freeDate[i]), freeMessage[i]);
            }
        }
        
        public static string Reader(string path)
        {
            string str = null;
            if (Directory.Exists(@"..\..\..\Files"))
            {
                if (File.Exists(path))
                {
                    using (StreamReader managerListStreamReader = new StreamReader(path))
                    {
                        str = managerListStreamReader.ReadToEnd();
                    }
                }

                //Maybe not need
                else
                {
                    using (File.Create(path));
                    using (StreamReader managerListStreamReader = new StreamReader(path))
                    {                        
                        str = managerListStreamReader.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(@"..\..\..\Files");
                Reader(path);
            }
            return str;
        }
        public static void Writer(string path, DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }

        
        public static void EmployeeListWriter(string name, string role)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(employeeListFile, true))
            {
                employeeListStreamWriter.WriteLine($"{name}, {role}");
            }
        }
    }  
}
