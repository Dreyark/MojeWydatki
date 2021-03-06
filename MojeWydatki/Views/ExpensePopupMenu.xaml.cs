﻿using MojeWydatki.Data;
using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ExpensePopupMenu : PopupPage
    {
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, EventArgs.Empty);
        public ExpenseViewModel ExpenseVM { get; set; }
        public ExpensePopupMenu(ExpenseViewModel vm)
        {
            ExpenseVM = vm;
            InitializeComponent();
        }

        private async void Background_tapped(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        async private void AddExpense_Clicked(object sender, EventArgs e)
        {
            var addExpenseView = new AddExpenseView();
            addExpenseView.BindingContext = ExpenseVM;
            await CloseAllPopup();
            await ((AppMasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(addExpenseView);
        }

        private async Task CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private void RemoveExpense_Clicked(object sender, EventArgs e)
        {
            Background_tapped(sender, e);
        }
    }
}