using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayesian_Task_Value
{
    class Task
    {
        public int Id { get; set; }
        private string PrivateName;
        public string Name { get => PrivateName + " (" + Value[1] + ")"; set => PrivateName = value; }
        public int Deadline { get; set; }
        public double[] Priority { get; set; }      
        public double[] Experience { get; set; }
        public double[] Complexity { get; set; }
        public double[] Effort { get; set; }
        public double[] Time { get; set; }
        public double[] Labor { get; set; }
        public double[] Challenges { get; set; }
        public double[] Value { get; set; }

        public List<string> Items { get; set; }

    }
}
