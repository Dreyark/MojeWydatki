using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MojeWydatki.Models;
using System.Linq;
using System.Collections.ObjectModel;

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
            Sprawdzam();
        }


        public void SeedsAsync()
        {
            var rows =  _database.Table<Category>().CountAsync().Result;
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
    }
}
