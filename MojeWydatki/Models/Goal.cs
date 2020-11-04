using MojeWydatki.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.Models
{
    public class Goal : SModel
    {
        public String Title { get; set; }
        public Double CurrentValue { get; set; }
        public Double GoalValue { get; set; }
        public Double Progress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFinished { get; set; }

        public Goal()
        {
        }
    }
}
