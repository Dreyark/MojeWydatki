﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using MojeWydatki.Data;
using MojeWydatki.Models;
using static MojeWydatki.Data.Database;

namespace MojeWydatki.ViewModels
{

    public class MonthStatsViewModel
    {

        public List<StatsByCategory> categoryChart;
        public List<ExtendedExpense> ExtendedExpenseList;
        public List<DateTime> dateList;
        public List<String> CategoryList;
        ExpenseRepository expenseRep;
        CategoryRepository categoryRep;
        public MonthStatsViewModel()
        {
            expenseRep = new ExpenseRepository();
            categoryRep = new CategoryRepository();

        }

        public void ExpensesList()
        {
            ExtendedExpenseList = new List<ExtendedExpense>();
            dateList = new List<DateTime>();
            CategoryList = new List<String>();
            var iList = expenseRep.GetExpensesAsync();
            var CatList = categoryRep.GetCategoriesAsync();
            foreach (Category i in CatList.Result)
            {
                CategoryList.Add(i.CategoryTitle);
            }

            foreach (Expense i in iList.Result.OrderByDescending(i=>i.Date))
            {
                if (!dateList.Contains(new DateTime(i.Date.Year, i.Date.Month, 1)))
                {
                    dateList.Add(new DateTime(i.Date.Year, i.Date.Month, 1));
                }

                ExtendedExpenseList.Add(new ExtendedExpense
                {
                    Expense = i,
                    Category = CategoryList.ElementAt(i.CategoryId - 1)
                });
            }
        }

        public void MakeStatsList(DateTime date)
        {
            categoryChart = new List<StatsByCategory>();
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            foreach (StatsByCategory i in App.Database.StatsByCategoryList(firstDayOfMonth, lastDayOfMonth))
            {
                categoryChart.Add(i);
            }
        }

        //    public void MostExpensiveExpenses()
        //    {
        //        BiggestExpensesList = new List<ExpenseWithCategory>();
        //        var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
        //        foreach (ExpenseWithCategory i in App.Database.List(firstDayOfMonth, lastDayOfMonth))
        //        {
        //            BiggestExpensesList.Add(i);
        //        }
        //    }



        //}

        public class StatsByCategory
        {
            public Decimal Value { get; set; }
            public String Category { get; set; }
        }

        //public class ExpenseWithCategory
        //{
        //    public String Title { get; set; }
        //    public Decimal Value { get; set; }
        //    public String Category { get; set; }
        //}
    }
}