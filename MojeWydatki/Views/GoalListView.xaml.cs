using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalListView : TabbedPage
    {
        public GoalListView()
        {
            BindingContext = new GoalListViewModel();
            InitializeComponent();

        }
        async private void AddGoal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddGoalView());
        }

        protected override async void OnAppearing()
        {
            var vm = BindingContext as GoalListViewModel;
            InProgresslistView.ItemsSource = null;
            CompletelistView.ItemsSource = null;
            await vm.MakeGoalList();
            InProgresslistView.ItemsSource = vm.GoalList.Where(i => i.IsFinished == false);
            CompletelistView.ItemsSource = vm.GoalList.Where(i => i.IsFinished == true);
            base.OnAppearing();
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Goal tappedGoalItem = e.Item as Goal;

            var GoalViewModelVM = new GoalViewModel(tappedGoalItem);
            var GoalPopupMenu = new UpdateGoalPopup(tappedGoalItem);
            GoalPopupMenu.CallbackEvent += (object sender, object e) => CallbackMethod();
            GoalPopupMenu.BindingContext = GoalViewModelVM;

            await PopupNavigation.Instance.PushAsync(GoalPopupMenu);
        }

        private void CallbackMethod()
        {
            OnAppearing();
        }
    }
}
