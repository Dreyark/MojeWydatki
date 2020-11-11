using MojeWydatki.Data;
using MojeWydatki.Data.Interface;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.ViewModels
{
    public class HomeViewModel
    {
        ExpenseRepository expenseRep;

        BudgetRepository budgetRep;

        public Double ExpensesValue;

        public Double MonthBalance;
        public HomeViewModel()
        {
            App.Database.SeedsAsync();
            expenseRep = new ExpenseRepository();
            budgetRep = new BudgetRepository();

            SetBudgetCommand = new Command(async () =>
            {
                var budget = new Budget();
                budget.MonthlyBudget = Convert.ToDouble(TheBudgetValue);
                budget.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                await budgetRep.SaveBudgetAsync(budget);
            });
        }

        public void setBalance()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            var test = budgetRep.GetBudgetAsync(firstDayOfMonth);
            ExpensesValue = 0;
            var iexpenseList = expenseRep.GetExpensesAsync().Result;
            var ibudgetList = budgetRep.GetExpensesAsync().Result;
            foreach (Expense i in iexpenseList)
            {
                if(i.Date >= firstDayOfMonth && i.Date <= lastDayOfMonth)
                    ExpensesValue += i.Value;
            }
            foreach (Budget i in ibudgetList)
            {
                if (i.Date == firstDayOfMonth)
                {
                    MonthBalance = i.MonthlyBudget - ExpensesValue;
                    break;
                }
            }
        }

        public String budgetValue;

        public String TheBudgetValue { get; set; }
        public Command SetBudgetCommand { get; }
    }
}
