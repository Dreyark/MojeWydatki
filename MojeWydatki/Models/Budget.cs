using MojeWydatki.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.Models
{
    public class Budget : SModel
    {
        public Double MonthlyBudget { get; set; }
        public DateTime Date { get; set; }
    }
}
