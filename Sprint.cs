using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bayesian_Task_Value
{
    class Sprint
    {

        public int Id { get; set; }
        private string PrivateName;
        public String Name { get => PrivateName + " (" + ProbabilityOfSuccess * 100 + "%)"; set => PrivateName = value; }

        public double ProbabilityOfSuccess { get; set; }

        public int Deadline { get; set; }

        public double TaskValue { get; set; }

        public int  Speed { get; set; } 

        public List<Task> Items { get; set; }
        public void AddTask(Task item)
        {
            this.Items.Add(item);
            this.Deadline += item.Deadline;
        }

        public void InsertTask(int index, Task item)
        {
            this.Items.Insert(index, item);
            this.Deadline += item.Deadline;
        }

        public void RemoveTask(Task item)
        {
            this.Items.Remove(item);
            this.Deadline -= item.Deadline;
        }

        public void RemoveTaskAt(int index)
        {
            var item = this.Items.ElementAt<Task>(index);
            this.Items.RemoveAt(index);
            this.Deadline -= item.Deadline;
        }
    }
}
