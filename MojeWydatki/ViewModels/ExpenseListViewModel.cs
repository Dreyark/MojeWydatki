using MojeWydatki.Data;
using MojeWydatki.Models;
using MojeWydatki.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class ExpenseListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Expense> ExpenseList { get; set; }

        ExpenseRepository expenseRep;

        public ExpenseListViewModel(ExpenseListView activity)
        {
            expenseRep = new ExpenseRepository();
        }

        public async Task MakeExpenseList()
        {
            ExpenseList = new ObservableCollection<Expense>();
            var iList = await expenseRep.GetExpensesAsync();

            foreach (Expense i in iList)
            {
                ExpenseList.Add(i);
            }
        }
    }
}
