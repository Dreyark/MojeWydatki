using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.ViewModels
{
    public class ExtendedExpense
    {
        public Expense Expense { get; set; }
        public String Category { get; set; }
    }
}
