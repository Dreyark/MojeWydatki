﻿using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using Xamarin.Forms;
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
            var vm = BindingContext as ExpenseListViewModel;
            listView.ItemsSource = null;
            await vm.MakeExpenseList();
            listView.ItemsSource = vm.ExtendedExpenseList.OrderByDescending(i=>i.Expense.Date);
            base.OnAppearing();
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
