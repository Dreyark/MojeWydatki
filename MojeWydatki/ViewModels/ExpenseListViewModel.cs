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
using System.Linq;

namespace MojeWydatki.ViewModels
{
    public class ExpenseListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ExtendedExpense> ExtendedExpenseList { get; set; }
        public List<String> CategoryList { get; set; }

        private ExpenseRepository expenseRep;
        private CategoryRepository categoryRep;

        public ExpenseListViewModel()
        {
            expenseRep = new ExpenseRepository();
            categoryRep = new CategoryRepository();
        }

        public async Task MakeExpenseList()
        {
            ExtendedExpenseList = new ObservableCollection<ExtendedExpense>();
            CategoryList = new List<String>();
            var iList = await expenseRep.GetExpensesAsync();
            var CatList = await categoryRep.GetCategoriesAsync();
            foreach (Category i in CatList)
            {
                CategoryList.Add(i.CategoryTitle);
            }

            foreach (Expense i in iList)
            {
                ExtendedExpenseList.Add(new ExtendedExpense
                {
                    Expense = i,
                    Category = CategoryList.ElementAt(i.CategoryId-1)
                }) ;
            }
        }
    }
}
