using MojeWydatki.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.Models
{
    public class PlannedExpense : SModel
    {
        public int Repeatability { get; set; }
        public String Description { get; set; }
        public int CategoryId { get; set; }
        public Double Value { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime NextExpenseDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
