using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayesian_Task_Value
{
    class Resource
    {
        public int Id { get; set; }
        public string ManagementExperience { get; set; } // Kinh nghiệm quản lí
        public int HumanSkill { get; set; } // Năng lực nhân viên
        public int Productivity { get; set; }// Năng suất
        public string TimePressure { get; set; } // Áp lực thời gian

        public Resource()
        {

        }

        public Resource(int id,string exp,int skill,int pro, string tim)
        {
            this.Id = id;
            this.ManagementExperience = exp;
            this.HumanSkill = skill;
            this.Productivity = pro;
            this.TimePressure = TimePressure;
        }
    }
}
