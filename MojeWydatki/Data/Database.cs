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
            //Seeds();
            Sprawdzam();
        }


        public void Seeds()
        {
            var ListItem = new List<Category>();
            ListItem.Add(new Category { CategoryTitle = "Zakupy" });
            ListItem.Add(new Category { CategoryTitle = "Elektronika" });
            ListItem.Add(new Category { CategoryTitle = "Czynsz" });
            ListItem.Add(new Category { CategoryTitle = "Transport" });
            _database.InsertAllAsync(ListItem);
        }
        public Task<List<Category>> CatList()
        {

            //string queryString = $"SELECT CategoryTitle FROM Category";
            //return await _database.QueryAsync<Category>(queryString).ConfigureAwait(false);
            return _database.Table<Category>().ToListAsync();

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
