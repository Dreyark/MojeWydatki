using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
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
                expense.Value = Convert.ToDouble(TheValue);
                expense.Date = DateTime.Now;
                expense.CategoryId = CategoryId;
                await expenseRep.SaveExpenseAsync(expense);
                TheDescription = string.Empty;
                TheValue = "0";
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
            TheValue = Convert.ToString(expenses.Value);
            CategoryId = expenses.CategoryId;
            System.Diagnostics.Debug.WriteLine("DATE : " + expenses.Date.ToString("dd-MM-yyyy"));

            SaveExpenseCommand = new Command(async () =>
            {
                expenses.Description = TheDescription;
                expenses.Value = Convert.ToDouble(TheValue);
                expenses.Date = DateTime.UtcNow;
                expenses.CategoryId = CategoryId;
                await expenseRep.SaveExpenseAsync(expenses);
                TheDescription = string.Empty;
                TheValue = "0";
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

        string value;
        public string TheValue
        {
            get => value;
            set
            {
                this.value = value;
                if(this.value.Last() == '.')
                {
                    this.value = this.value.Remove(this.value.Length - 1);
                }
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
