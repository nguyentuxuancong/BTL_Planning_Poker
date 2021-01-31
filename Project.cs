using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bayesian_Task_Value
{
    class Project
    {
        public int Id { get; set; }
        private string PrivateName;
        public String Name { get => PrivateName + " (" + ProbabilityOfSuccess*100 + "%)"; set => PrivateName = value; }


        public double ProbabilityOfSuccess { get; set; }

        public int Deadline { get; private set; }

        public List<Sprint> Items { get; set; }


        public Project(int id, String name)
        {
            this.Items = new List<Sprint>();
            this.Deadline = 0;
            this.Id = id;
            this.PrivateName = name;
        }

        public void AddSprint(Sprint item)
        {
            this.Items.Add(item);
            this.Deadline += item.Deadline;
        }

        public void InsertSprint(int index, Sprint item)
        {
            this.Items.Insert(index, item);
            this.Deadline += item.Deadline;
        }

        public void RemoveSprint(Sprint item)
        {
            this.Items.Remove(item);
            this.Deadline -= item.Deadline;
        }

        public void RemoveSprintAt(int index)
        {
            var item = this.Items.ElementAt<Sprint>(index);
            this.Items.RemoveAt(index);
           this.Deadline += item.Deadline;
        }

    }
}
