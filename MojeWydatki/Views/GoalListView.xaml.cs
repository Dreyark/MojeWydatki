using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalListView : ContentPage
    {
        public ObservableCollection<Goal> GoalList { get; set; }

        public GoalListView()
        {
            BindingContext = new GoalListViewModel(this);
            GoalList = new ObservableCollection<Goal>();
            InitializeComponent();

        }
        async private void AddGoal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddGoalView());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as GoalListViewModel;

            vm.GetDataGoal.Execute(GoalList);
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Goal tappedGoalItem = e.Item as Goal;

            var createGoalVM = new GoalViewModel(tappedGoalItem);

            //var createGoalPage = new AddGoalView();

            //createGoalPage.BindingContext = createGoalVM;

            //await Navigation.PushAsync(createGoalPage);

            //System.Diagnostics.Debug.WriteLine("SELECTED : " + tappedGoalItem.ID);
        }

        //private void RemoveGoal_Clicked(object sender, EventArgs e)
        //{
        //    var button = sender as Button;
        //    var Goal = button.BindingContext as Goal;
        //    var vm = BindingContext as GoalListViewModel;
        //    vm.RemoveGoal.Execute(Goal);
        //}

    }
}
