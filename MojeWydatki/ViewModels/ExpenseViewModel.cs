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
        public IList<Category> CategoryList { get; set; }

        ExpenseRepository expenseRep;
        CategoryRepository catRep;

        public ExpenseViewModel()
        {
            expenseRep = new ExpenseRepository();
            catRep = new CategoryRepository();
            CategoryList = new ObservableCollection<Category>();

            var iList = catRep.GetCategoriesAsync();
            foreach (Category i in iList.Result)
            {
                CategoryList.Add(i);
            }

            SaveExpenseCommand = new Command(async () =>
            {
                var expense = new Expense();
                TheDate += TheTime;
                expense.Description = TheDescription;
                expense.Value = Convert.ToDouble(TheValue);
                expense.Date = TheDate;
                expense.CategoryId = CategoryId+1;
                await expenseRep.SaveExpenseAsync(expense);
                TheDescription = string.Empty;
                TheValue = "0";
                CategoryId = -1;
            });
        }

        public ExpenseViewModel(ExtendedExpense extexpense)
        {
            var expense = extexpense.Expense;
            expenseRep = new ExpenseRepository();
            catRep = new CategoryRepository();
            CategoryList = new ObservableCollection<Category>();

            var iList = catRep.GetCategoriesAsync();
            foreach (Category i in iList.Result)
            {
                CategoryList.Add(i);
            }
            var Id = expense.ID;
            TheDate = expense.Date;
            TheTime = expense.Date.TimeOfDay;
            TheCategory = extexpense.Category;
            TheDescription = expense.Description;
            TheValue = Convert.ToString(expense.Value);
            CategoryId = expense.CategoryId-1;
            //System.Diagnostics.Debug.WriteLine("DATE : " + expense.Date.ToString("dd-MM-yyyy"));

            SaveExpenseCommand = new Command(async () =>
            {

                TheDate = TheDate.Date;
                TheDate += TheTime;
                expense.ID = Id;
                expense.Description = TheDescription;
                expense.Value = Convert.ToDouble(TheValue);
                expense.Date = TheDate;
                expense.CategoryId = CategoryId+1;
                await expenseRep.SaveExpenseAsync(expense);
                TheDescription = string.Empty;
                TheValue = string.Empty;
                CategoryId = -1;
            });

            RemoveExpense = new Command(async () =>
            {
                await expenseRep.DeleteExpenseAsync(expense);
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

        string category;
        public string TheCategory
        {
            get => category;
            set
            {
                category = value;
                var args = new PropertyChangedEventArgs(nameof(TheCategory));
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
                if (this.value == "")
                {
                    this.value = "0";
                }
                else
                {
                    if (this.value.Last() == '.')
                    {
                        this.value = this.value.Remove(this.value.Length - 1);
                    }
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

        DateTime date;
        public DateTime TheDate
        {
            get
            {
                return date;
            }
            set
            {
                this.date = value;
                //System.Diagnostics.Debug.WriteLine(this.categoryId);
                var args = new PropertyChangedEventArgs(nameof(TheDate));
                PropertyChanged?.Invoke(this, args);
            }
        }

        TimeSpan time;
        public TimeSpan TheTime
        {
            get
            {
                return time;
            }
            set
            {
                this.time = value;
                //System.Diagnostics.Debug.WriteLine(this.categoryId);
                var args = new PropertyChangedEventArgs(nameof(TheTime));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public Command RemoveExpense { get; }
        public Command SaveExpenseCommand { get; }
    }
}
