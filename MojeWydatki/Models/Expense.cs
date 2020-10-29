using MojeWydatki.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.Models
{
    public class Expense : SModel
    {
        public String Description { get; set; }
        public Category Category { get; set; }
        public Double Value { get; set; }
        public DateTime Date { get; set; }

        public Expense(string text, DateTime date)
        {
            Description = text;
            Date = date;
        }

        public Expense()
        {
        }
    }
}
