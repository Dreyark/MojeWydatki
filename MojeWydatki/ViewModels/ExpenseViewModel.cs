using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class ExpenseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command GetDataCategory { get; }
        public IList<Category> CategoryList { get; set; }

        ExpenseRepository expenseRep;

        public ExpenseViewModel()
        {
            expenseRep = new ExpenseRepository();
            CategoryList = new ObservableCollection<Category>();

            var iList = App.Database.CatList();
            foreach (Category i in iList.Result)
            {
                CategoryList.Add(i);
            }

            SaveExpenseCommand = new Command(async () =>
            {
                var expense = new Expense();
                expense.Description = TheDescription;
                expense.Value = TheValue;
                expense.Date = DateTime.UtcNow;
                expense.CategoryId = CategoryId;
                await expenseRep.SaveExpenseAsync(expense);
                TheDescription = string.Empty;
                TheValue = 0;
                CategoryId = -1;
            });
        }

        public ExpenseViewModel(Expense expenses)
        {
            expenseRep = new ExpenseRepository();
            CategoryList = new ObservableCollection<Category>();

            var iList = App.Database.CatList();
            foreach (Category i in iList.Result)
            {
                CategoryList.Add(i);
            }

            TheDescription = expenses.Description;
            TheValue = expenses.Value;
            CategoryId = expenses.CategoryId;
            System.Diagnostics.Debug.WriteLine("DATE : " + expenses.Date.ToString("dd-MM-yyyy"));

            SaveExpenseCommand = new Command(async () =>
            {
                expenses.Description = TheDescription;
                expenses.Value = TheValue;
                expenses.Date = DateTime.UtcNow;
                expenses.CategoryId = CategoryId;
                await expenseRep.SaveExpenseAsync(expenses);
                TheDescription = string.Empty;
                TheValue = 0;
                CategoryId = -1;
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

        int categoryId;
        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                this.categoryId = value;
                //System.Diagnostics.Debug.WriteLine(this.categoryId);
                var args = new PropertyChangedEventArgs(nameof(CategoryList));
                PropertyChanged?.Invoke(this, args);
            }
        }


        public Command SaveExpenseCommand { get; }
        public Expense BindingContext { get; }
    }
}
