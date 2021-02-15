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
    public partial class PlannedExpenseListView : ContentPage
    {
        public PlannedExpenseListView()
        {
            BindingContext = new PlannedExpenseListViewModel();
            InitializeComponent();
        }

        async private void AddPlannedExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPlannedExpenseView());
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            PlannedExpense tappedExpenseItem = e.Item as PlannedExpense;
            var PlannedExpenseVM = new PlannedExpenseViewModel(tappedExpenseItem);
            var plannedExpense = new AddPlannedExpenseView();

            plannedExpense.BindingContext = PlannedExpenseVM;

            await Navigation.PushAsync(plannedExpense);

        }

        protected override async void OnAppearing()
        {
            var vm = BindingContext as PlannedExpenseListViewModel;
            listView.ItemsSource = null;
            await vm.MakePlannedExpenseList();
            listView.ItemsSource = vm.PlannedList;
            base.OnAppearing();
        }
    }
}