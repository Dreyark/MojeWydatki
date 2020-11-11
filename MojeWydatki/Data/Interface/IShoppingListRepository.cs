using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data.Interface
{
    public interface IShoppingListRepository
    {
        public Task<List<ShoppingList>> GetShoppingListsAsync();
        public Task GetShoppingListAsync(int id);
        public Task SaveShoppingListAsync(ShoppingList shoppingList);
        public Task DeleteShoppingListAsync(ShoppingList shoppingList);
    }
}
