using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSalary
{
    public class AllCurrentData
    {
        public DateTime date { get; set; }
        public string name { get; set; }
        public byte hours { get; set; }
        public decimal pay { get; set; }
        public string message { get; set; }
        public Settings.Role role { get; set; }
    }
}
