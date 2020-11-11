using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{

    public partial class ExpenseListView : ContentPage
    {
        public ExpenseListView()
        {
            BindingContext = new ExpenseListViewModel(this);
            InitializeComponent();
        }

        async private void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpenseView());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ExpenseListViewModel;
            await vm.MakeExpenseList();
            listView.ItemsSource = vm.ExpenseList;
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Expense tappedExpenseItem = e.Item as Expense;

            var expensePopupMenuVM = new ExpenseViewModel(tappedExpenseItem);
            var expensePopupMenu = new ExpensePopupMenu(tappedExpenseItem);

            expensePopupMenu.CallbackEvent += (object sender, object e) => CallbackMethod();
            expensePopupMenu.BindingContext = expensePopupMenuVM;

            await PopupNavigation.Instance.PushAsync(expensePopupMenu);

        }

        private void CallbackMethod()
        {
            OnAppearing();
        }
    }
}
