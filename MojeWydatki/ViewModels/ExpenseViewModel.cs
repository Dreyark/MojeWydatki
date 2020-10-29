using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ExpenseViewModel()
        {
            SaveExpenseCommand = new Command(async () =>
            {
                var expense = new Expense();
                expense.Description = TheDescription;
                expense.Value = TheValue;
                expense.Date = DateTime.UtcNow;
                await App.Database.SaveExpenseAsync(expense);
                TheDescription = string.Empty;
                TheValue = 0;
            });
        }

        public ExpenseViewModel(Expense expenses)
        {
            TheDescription = expenses.Description;
            TheValue = expenses.Value;
            System.Diagnostics.Debug.WriteLine("DATE : " + expenses.Date.ToString("dd-MM-yyyy"));

            SaveExpenseCommand = new Command(async () =>
            {
                expenses.Description = TheDescription;
                expenses.Value = TheValue;
                expenses.Date = DateTime.UtcNow;
                await App.Database.SaveExpenseAsync(expenses);
                TheDescription = string.Empty;
                TheValue = 0;
            });
        }

        string description;
        public string TheDescription
        {
            get => description;
            set
            {
                description = value;
                var args = new PropertyChangedEventArgs(nameof(TheDescription));
                PropertyChanged?.Invoke(this, args);
            }
        }

        //public string TheCategory
        //{
        //    get => theExpense;
        //    set
        //    {
        //        theExpense = value;
        //        var args = new PropertyChangedEventArgs(nameof(TheExpense));
        //        PropertyChanged?.Invoke(this, args);
        //    }
        //}
        double value;
        public double TheValue
        {
            get => value;
            set
            {
                this.value = value;
                var args = new PropertyChangedEventArgs(nameof(TheValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command SaveExpenseCommand { get; }
        public Expense BindingContext { get; }
    }
}
