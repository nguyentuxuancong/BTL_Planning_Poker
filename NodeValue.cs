using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Bayesian_Task_Value
{
    class NodeValue
    {
        private String name;
        private Dictionary<String, double> value = new Dictionary<string, double>();
        private List<String> valueName = new List<string>();

        public NodeValue(String name, List<String> valueName)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (valueName == null)
                throw new ArgumentNullException(nameof(valueName));
            this.name = name;
            foreach(string s in valueName)
            {
                value[s] = 1.0f / valueName.Count;
                this.valueName.Add(s);
            }
        }

        public NodeValue(String name, String[] valueName) : this(name, new List<string>(valueName))
        {
        }

        public void setValue(String name, double value)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name)); 
            this.value[name] = value;
        }

        public void setValue(int index, double value)
        {
            this.setValue(this.valueName[index], value);
        }
       
        public void setValue(double[] values)
        {
            for (int i = 0; i < valueName.Count; ++i)
                setValue(i, values[i]);
        }
        public double getValue(String name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            return this.value[name];
        }
        public double getValue(int index)
        {
            return this.getValue(this.valueName[index]);
        }

        public double this[string name]
        {
            get => getValue(name);
        }

        public double this[int id]
        {
            get => getValue(id);
        }
    }
}
