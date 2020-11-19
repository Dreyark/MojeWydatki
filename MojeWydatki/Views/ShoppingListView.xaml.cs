using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListView : ContentPage
    {
        ShoppingList tapped;
        List<String> Lista = new List<String>();
        public ShoppingListView(ShoppingList shoppingList)
        {
            tapped = shoppingList;
            InitializeComponent();
            MakeShoppingList();
        }

        public void MakeShoppingList()
        {
            Lista = new List<String>();
            string[] words = tapped.Products.Split('¡');
            foreach (string word in words)
            {
                if(word.Length != 0)
                    Lista.Add(word);
            }
            listView.ItemsSource = Lista;
        }

        public void DeleteProduct_Clicked(object sender, EventArgs e)
        {
                var button = sender as Button;
                var note = button.BindingContext as String;
            Lista.Remove(note);
            UpdateProducts();
            MakeShoppingList();
        }

        public void UpdateProducts()
        {
            tapped.Products = "";
            foreach (String product in Lista)
            {
                if(product.Length != 0)
                    tapped.Products += "¡" + product;
            }
            var vm = new ShoppingListViewModel(tapped);
            vm.SaveShoppingListCommand.Execute(tapped);
        }

        public void SaveProductList_Clicked(object sender, EventArgs e)
        {
            tapped.Products += "¡" + ProductEditor.Text;
            ProductEditor.Text = "";
            var vm = new ShoppingListViewModel(tapped);
            vm.SaveShoppingListCommand.Execute(tapped);
            MakeShoppingList();
        }
    }
}