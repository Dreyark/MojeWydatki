using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.ViewModels
{
    public class ShoppingListListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ShoppingList> ShoppingList { get; set; }

        ShoppingListRepository ShopRep;

        public ShoppingListListViewModel()
        {
            ShopRep = new ShoppingListRepository();
        }

        public async Task MakeShoppingListList()
        {
            ShoppingList = new ObservableCollection<ShoppingList>();
            var iList = await ShopRep.GetShoppingListsAsync();

            foreach (ShoppingList i in iList)
            {
                ShoppingList.Add(i);
            }
        }
    }
}
