using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<ExpenseWithCategory> BiggestExpensesList;
        public MonthStatsViewModel()
        {

        }
        public void MakeStatsList()
        {
            categoryChart = new List<StatsByCategory>();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            foreach (StatsByCategory i in App.Database.StatsByCategoryList(firstDayOfMonth, lastDayOfMonth))
            {
                categoryChart.Add(i);
            }
        }

        public void MostExpensiveExpenses()
        {
            BiggestExpensesList = new List<ExpenseWithCategory>();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            foreach (ExpenseWithCategory i in App.Database.List(firstDayOfMonth, lastDayOfMonth))
            {
                BiggestExpensesList.Add(i);
            }
        }



    }

    public class StatsByCategory
    {
        public Decimal Value { get; set; }
        public String Category { get; set; }
    }

    public class ExpenseWithCategory
    {
        public String Title { get; set; }
        public Decimal Value { get; set; }
        public String Category { get; set; }
    }
}
