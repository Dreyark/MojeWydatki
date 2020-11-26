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
using static MojeWydatki.ViewModels.ExpenseListViewModel;

namespace MojeWydatki.Views
{

    public partial class ExpenseListView : ContentPage
    {
        public ExpenseListView()
        {
            BindingContext = new ExpenseListViewModel();
            InitializeComponent();
        }

        async private void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExpenseView());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ExpenseListViewModel;
            await vm.MakeExpenseList();
            listView.ItemsSource = vm.ExtendedExpenseList.OrderByDescending(i=>i.Expense.Date);
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ExtendedExpense tappedExpenseItem = e.Item as ExtendedExpense;
            Expense expense = tappedExpenseItem.Expense;
            var ExpenseVM = new ExpenseViewModel(tappedExpenseItem);
            var expensePopupMenu = new ExpensePopupMenu(ExpenseVM);

            expensePopupMenu.CallbackEvent += (object sender, object e) => CallbackMethod();
            expensePopupMenu.BindingContext = ExpenseVM;

            await PopupNavigation.Instance.PushAsync(expensePopupMenu);

        }

        private void CallbackMethod()
        {
            OnAppearing();
        }
    }
}
