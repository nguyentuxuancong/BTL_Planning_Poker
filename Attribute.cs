using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayesian_Task_Value
{
    class Attribute
    {
        public NodeValue priority { get; set; }
        public NodeValue experience { get; set; }
        public NodeValue complexity { get; set; }
        public NodeValue effort { get; set; }
        public NodeValue time { get; set; }
        public NodeValue labor { get; set; }
        public NodeValue challenges { get; set; }
        public NodeValue taskValue { get; set; }

        Dictionary<String, NodeValue> dict = new Dictionary<string, NodeValue>();

        public Attribute(NodeValue priority = null, NodeValue experience = null, NodeValue complexity = null, NodeValue effort = null, NodeValue time = null)
        {
            this.priority = priority ?? new NodeValue("Priority", new String[] { "Low", "Medium", "High" });
            this.experience = experience ?? new NodeValue("Priority", new String[] { "Low", "Medium", "High" });
            this.complexity = complexity ?? new NodeValue("Priority", new String[] { "Low", "Medium", "High" });
            this.effort = effort ?? new NodeValue("Priority", new String[] { "Low", "Medium", "High" });
            this.time = time ?? new NodeValue("Priority", new String[] { "Low", "Medium", "High" });
            this.labor = new NodeValue("Labor", new String[] { "Low", "High" });
            this.challenges = new NodeValue("Labor", new String[] { "Low", "High" });
            this.taskValue = new NodeValue("Labor", new String[] { "Low", "High" });
            dict["Priority"] = priority;
            dict["Experience"] = experience;
            dict["Complexity"] = complexity;
            dict["Effort"] = effort;
            dict["Time"] = time;
        }


    }
}
