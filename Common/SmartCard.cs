using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SmartCard
    {
        string subjectName;
        int pin;

        public string SubjectName { get => subjectName; set => subjectName = value; }
        public int Pin { get => pin; set => pin = value; }

        public SmartCard(string subjectName, int pin)
        {
            this.subjectName = subjectName;
            this.pin = pin;
        }
    }
}
