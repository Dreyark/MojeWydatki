using MojeWydatki.Data.Interface;
using MojeWydatki.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data
{
    class ShoppingListRepository : IShoppingListRepository
    {
        readonly SQLiteAsyncConnection _database;

        public ShoppingListRepository()
        {
            _database = new SQLiteAsyncConnection(App.DbPath);
            _database.CreateTableAsync<ShoppingList>().Wait();
        }
        public Task DeleteShoppingListAsync(ShoppingList shoppingList)
        {
            return _database.DeleteAsync(shoppingList);
        }

        public Task GetShoppingListAsync(int id)
        {
            return _database.Table<ShoppingList>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<ShoppingList>> GetShoppingListsAsync()
        {
            return _database.Table<ShoppingList>().ToListAsync();
        }

        public Task SaveShoppingListAsync(ShoppingList shoppingList)
        {
            if (shoppingList.ID != 0)
            {
                return _database.UpdateAsync(shoppingList);
            }
            else
            {
                return _database.InsertAsync(shoppingList);
            }
        }
    }
}
