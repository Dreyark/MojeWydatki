using MojeWydatki.Data;
using MojeWydatki.Models;
using MojeWydatki.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
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

            GetDataExpense = new Command(async () =>
            {
                ExpenseList = new ObservableCollection<Expense>();

                var iList = await expenseRep.GetExpensesAsync();

                foreach (Expense i in iList)
                {
                    ExpenseList.Add(i);
                    activity.listView.ItemsSource = ExpenseList;
                }

            });

            RemoveExpense = new Command<Expense>(async (Expenses) =>
            {
                await expenseRep.DeleteExpenseAsync(Expenses);
                ExpenseList.Remove(Expenses);
                System.Diagnostics.Debug.WriteLine("Data Remove : " + Expenses.Description);
            });
        }

        public Command GetDataExpense { get; }

        public Command<Expense> RemoveExpense { get; }
    }
}
