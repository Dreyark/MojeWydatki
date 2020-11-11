using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    class ShoppingListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ShoppingListRepository ShopRep;
        public ShoppingListViewModel()
        {
            ShopRep = new ShoppingListRepository();

            SaveShoppingListCommand = new Command(async () =>
            {
                var shoppingList = new ShoppingList();
                shoppingList.ListName = TheListName;
                shoppingList.Products = "";
                await ShopRep.SaveShoppingListAsync(shoppingList);
                TheListName = string.Empty;
                TheProducts = string.Empty;
            });

        }
        public ShoppingListViewModel(ShoppingList shoppingList)
        {
            ShopRep = new ShoppingListRepository();

            TheListName = shoppingList.ListName;
            TheProducts = shoppingList.Products;

            SaveShoppingListCommand = new Command(async () =>
            {
                shoppingList.ListName = TheListName;
                shoppingList.Products = TheProducts;
                await ShopRep.SaveShoppingListAsync(shoppingList);
                TheListName = string.Empty;
                TheProducts = string.Empty;
            });

            RemoveShoppingList = new Command(async () =>
            {
                await ShopRep.DeleteShoppingListAsync(shoppingList);
            });
        }

        string listName;
        public string TheListName
        {
            get => listName;
            set
            {
                listName = value;
                var args = new PropertyChangedEventArgs(nameof(TheListName));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string products;
        public string TheProducts
        {
            get => products;
            set
            {
                products = value;
                var args = new PropertyChangedEventArgs(nameof(TheProducts));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command RemoveShoppingList { get; }
        public Command SaveShoppingListCommand { get; }
    }
}
