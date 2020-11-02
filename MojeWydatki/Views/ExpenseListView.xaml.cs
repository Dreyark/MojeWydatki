using MojeWydatki.Models;
using MojeWydatki.ViewModels;
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
        public ObservableCollection<Expense> ExpenseList { get; set; }
        public ExpenseListView()
        {
            BindingContext = new ExpenseListViewModel(this);
            ExpenseList = new ObservableCollection<Expense>();
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

            vm.GetDataExpense.Execute(ExpenseList);
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Expense tappedExpenseItem = e.Item as Expense;

            var createExpenseVM = new ExpenseViewModel(tappedExpenseItem);

            var createExpensePage = new ExpenseView();

            createExpensePage.BindingContext = createExpenseVM;

            await Navigation.PushAsync(createExpensePage);

            System.Diagnostics.Debug.WriteLine("SELECTED : " + tappedExpenseItem.ID);
        }

        private void RemoveExpense_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var expense = button.BindingContext as Expense;
            var vm = BindingContext as ExpenseListViewModel;
            vm.RemoveExpense.Execute(expense);
        }
    }
}
