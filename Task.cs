using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayesian_Task_Value
{
    class Task
    {
        public String name { get; }
        public int id { get; }
        public Attribute attribute {get;}
        public string high { get; private set; }
        public string low { get; private set; }
        public Task(int id, String name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.id = id;
            attribute = new Attribute();

        }

        public void caculateValue()
        {
            Net.caculateTaskValue(this);
            this.high = String.Format("{0,12:P2}", attribute.taskValue.getValue("High"));
            this.low = String.Format("{0,12:P2}", attribute.taskValue.getValue("Low"));
        }
        public override string ToString()
        {
            return name + " {" + attribute.taskValue["Low"] + "," + attribute.taskValue["High"] + "}.";
        }
    }
}
