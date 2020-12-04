using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MojeWydatki.Models;
using System.Linq;
using System.Collections.ObjectModel;
using MojeWydatki.ViewModels;

namespace MojeWydatki.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();
            _database.CreateTableAsync<Category>().Wait();
            _database.CreateTableAsync<Goal>().Wait();
            _database.CreateTableAsync<Budget>().Wait();
            _database.CreateTableAsync<ShoppingList>().Wait();
            _database.CreateTableAsync<Debt>().Wait();
            _database.CreateTableAsync<PlannedExpense>().Wait();
        }


        public void SeedsAsync()
        {
            var rows = _database.Table<Category>().CountAsync().Result;
            if (rows == 0)
            {
                var ListItem = new List<Category>();
                ListItem.Add(new Category { CategoryTitle = "Zakupy" });
                ListItem.Add(new Category { CategoryTitle = "Elektronika" });
                ListItem.Add(new Category { CategoryTitle = "Czynsz" });
                ListItem.Add(new Category { CategoryTitle = "Transport" });
                _database.InsertAllAsync(ListItem);
            }
        }

        public async void Sprawdzam()
        {
            var myList = await _database.Table<Expense>().ToListAsync();
            foreach (var s in myList)
            {
                Console.WriteLine(s.CategoryId);
            }
        }

        public List<MonthStatsViewModel.StatsByCategory> StatsByCategoryList(DateTime From, DateTime To)
        {
            var q = _database.QueryAsync<MonthStatsViewModel.StatsByCategory>(
                @"Select Sum(Value) as Value, Category.CategoryTitle as Category from Category INNER JOIN Expense ON Category.ID = Expense.CategoryId WHERE
                Expense.Date > '" + From.Ticks + "' AND Expense.Date <'" + To.Ticks + "' Group By CategoryId");
            return q.Result;
        }

        //public List<ExpenseWithCategory> List(DateTime From, DateTime To)
        //{
        //    var q = _database.QueryAsync<ExpenseWithCategory>(
        //        @"Select Expense.Description as Title, Expense.Value as Value, Category.CategoryTitle as Category from Category INNER JOIN Expense ON Category.ID = Expense.CategoryId WHERE
        //        Expense.Date > '" + From.Ticks + "' AND Expense.Date <'" + To.Ticks + "' ORDER BY Value DESC LIMIT 10");
        //    return q.Result;
        //}

        public Summary ThreeMonthsStats(DateTime From, DateTime To)
        {
            Summary summary = new Summary();
            summary.Value = 0;
            summary.Budget = 0;
            summary.ValuePerDay = new Double[DateTime.DaysInMonth(From.Year,From.Month)];
            var expList = _database.Table<Expense>().ToListAsync().Result.Where(i => i.Date > From && i.Date  < To);
            var budList = _database.Table<Budget>().ToListAsync().Result.Where(i => i.Date == From);
            foreach (var s in expList)
            {
                summary.Value += s.Value;
                summary.ValuePerDay[s.Date.Day-1] += s.Value; 
            }
            foreach (var z in budList)
            {
                summary.Budget = z.MonthlyBudget;
            }
            summary.Date = From;
            return summary;
        }
    }
}
