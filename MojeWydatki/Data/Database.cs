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

        public List<StatsByCategory> StatsByCategoryList(DateTime From, DateTime To)
        {
            var q = _database.QueryAsync<StatsByCategory>(
                @"Select Sum(Value) as Value, Category.CategoryTitle as Category from Category INNER JOIN Expense ON Category.ID = Expense.CategoryId WHERE
                Expense.Date > '"+From.Ticks+"' AND Expense.Date <'"+To.Ticks+"' Group By CategoryId");
            return q.Result;
        }

        public List<ExpenseWithCategory> List(DateTime From, DateTime To)
        {
            var q = _database.QueryAsync<ExpenseWithCategory>(
                @"Select Expense.Description as Title, Expense.Value as Value, Category.CategoryTitle as Category from Category INNER JOIN Expense ON Category.ID = Expense.CategoryId WHERE
                Expense.Date > '" + From.Ticks + "' AND Expense.Date <'" + To.Ticks + "' ORDER BY Value DESC LIMIT 4");
            return q.Result;
        }
    }
}
