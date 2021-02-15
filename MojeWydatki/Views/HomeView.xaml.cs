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
using static MojeWydatki.ViewModels.ExpenseListViewModel;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        HomeViewModel vm;
        public HomeView()
        {
            vm = new HomeViewModel();
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var expList = new ExpenseListViewModel();
            vm.setBalance();
            SetupView();
            await expList.MakeExpenseList();
            var list = expList.ExtendedExpenseList.OrderByDescending(i=>i.Expense.Date).Take(5);
            listView.ItemsSource = null;
            listView.ItemsSource = list;
            vm.Height = list.Count() * 78;
            base.OnAppearing();
            BindingContext = vm;    
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ExtendedExpense tappedExtendedExpenseItem = e.Item as ExtendedExpense;
            var expensePopupMenuVM = new ExpenseViewModel(tappedExtendedExpenseItem);
            var expensePopupMenu = new ExpensePopupMenu(expensePopupMenuVM);

            expensePopupMenu.CallbackEvent += (object sender, object e) => CallbackMethod();
            expensePopupMenu.BindingContext = expensePopupMenuVM;

            await PopupNavigation.Instance.PushAsync(expensePopupMenu);

        }

        private void CallbackMethod()
        {
            OnAppearing();
        }

        public void SetupView()
        {
            if (vm.isBalanceSet == false)
            {
                BalanceLabel.Text = "Ustaw budżet";
            }
            else
            {
                BalanceLabel.Text = String.Format("{0:N2} zł", Convert.ToString(vm.MonthBalance));
            }
        }
        async private void AddCategory_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddCategoryPopup());
        }

        async private void AddExpense_Clicked(object sender, EventArgs e)
        {
            await ((AppMasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AddExpenseView());
        }

        async private void SetBudget_Clicked(object sender, EventArgs e)
        {
            var budgetPopup = new SetBudgetPopup();
            budgetPopup.CallbackEvent += (object sender, object e) => CallbackMethod();
            await PopupNavigation.Instance.PushAsync(budgetPopup);
        }
    }
}