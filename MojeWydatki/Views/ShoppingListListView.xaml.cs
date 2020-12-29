using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using Rg.Plugins.Popup.Services;
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
    public partial class ShoppingListListView : ContentPage
    {
        public ShoppingListListView()
        {
            BindingContext = new ShoppingListListViewModel();
            InitializeComponent();
        }
        async private void AddShoppingList_Clicked(object sender, EventArgs e)
        {
            var addShoppingListPopup = new AddShoppingListPopup();
            addShoppingListPopup.CallbackEvent += (object sender, object e) => CallbackMethod();
            await PopupNavigation.Instance.PushAsync(addShoppingListPopup);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ShoppingListListViewModel;
            await vm.MakeShoppingListList();
            listView.ItemsSource = vm.ShoppingList;
            
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ShoppingList tappedItem = e.Item as ShoppingList;

            var shoppingListVM = new ShoppingListViewModel(tappedItem);
            var shoppingListView = new ShoppingListView(tappedItem);
            shoppingListView.BindingContext = shoppingListVM;

            await Navigation.PushAsync(shoppingListView);

        }
        private void CallbackMethod()
        {
            OnAppearing();
        }
    }
}