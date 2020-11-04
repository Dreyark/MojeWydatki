using MojeWydatki.Data;
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
        ExpenseRepository expenseRep;
        Expense tappedExpenseItem;
        public ExpensePopupMenu(Expense tapped)
        {
            tappedExpenseItem = tapped;
            //Desc = tappedExpenseItem.Description;
            InitializeComponent();
        }

        public enum Orientation
        {
            Right_Down, Right_up, Left_Down, Left_Up
        }

        private async void Background_tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        async private void AddExpense_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopPopupAsync();
            //await Navigation.PushAsync(new ExpenseView());

            var expenseViewMV = new ExpenseViewModel(tappedExpenseItem);
            var expenseView = new ExpenseView();
            expenseView.BindingContext = expenseViewMV;
            CloseAllPopup();
            await ((AppMasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(expenseView);
            //await App.Current.MainPage.Navigation.PushAsync(new ExpenseView())
            //await CloseAllPopup();
        }

        private async Task CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
            await Navigation.PushAsync(new ExpenseView());
        }

        private void RemoveExpense_Clicked(object sender, EventArgs e)
        {
            expenseRep = new ExpenseRepository();
            expenseRep.DeleteExpenseAsync(tappedExpenseItem);
            Background_tapped(sender,e);
        }
    }
}