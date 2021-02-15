using MojeWydatki.Data;
using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateGoalPopup : PopupPage
    {
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, EventArgs.Empty);
        Goal tappedGoalItem;
        public UpdateGoalPopup(Goal goal)
        {
            tappedGoalItem = goal;
            InitializeComponent();
            SetVisible();
        }

        private void SetVisible()
        {
            if (tappedGoalItem.IsFinished)
            {
                GoalDescription.FontSize = 18;
                GoalDescription.Text = "Zebrano: " + tappedGoalItem.CurrentValue + " zł, w " + tappedGoalItem.EndDate.Subtract(tappedGoalItem.StartDate).Days + " dni." + System.Environment.NewLine;
                GoalDescription.Text += "Data Rozpoczęcia: " + tappedGoalItem.StartDate.ToString("dd-MM-yyyy") + System.Environment.NewLine + "Data zakończenia: " + tappedGoalItem.EndDate.ToString("dd-MM-yyyy");

                CurrentValueEntry.IsVisible = false;
                UpdateGoalButton.IsVisible = false;
            }
            else
            {
                var remainingDays = tappedGoalItem.EndDate.Subtract(DateTime.Now).Days;
                double perDay = (tappedGoalItem.GoalValue - tappedGoalItem.CurrentValue) / remainingDays;
                GoalDescription.Text = "Data zakończenia: " + tappedGoalItem.EndDate.Date.ToString("dd-MM-yyyy");
                if (remainingDays > 0)
                {
                    GoalDescription.Text += System.Environment.NewLine + "Pozostało:" + remainingDays + " dni.";
                    GoalDescription.Text += System.Environment.NewLine + "Musisz codziennie wpłacać po: " + Math.Round(perDay,2).ToString("N2") + " zł";
                }
                else
                {
                    GoalDescription.Text += System.Environment.NewLine + "Cel nie zostanie osiągnięty w założonym terminie";
                }
            }
        }

        private async void Background_tapped(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        async private void UpdateGoal_Clicked(object sender, EventArgs e)
        {

            await CloseAllPopup();
        }

        private async Task CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        void Entry_CurrentValueChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            if (e.NewTextValue == null)
            {

            }
            else
            {
                bool is2 = Regex.IsMatch(CurrentValueEntry.Text, @"^[0-9]+((\.|\,)[0-9]{0,2})?$|^$");
                if (!is2)
                {

                    CurrentValueEntry.Text = e.OldTextValue;

                }
            }
            if (newText == "")
            {

                UpdateGoalButton.IsVisible = false;

            }
            else
            {
                UpdateGoalButton.IsVisible = true;
            }
        }

        private void RemoveGoal_Clicked(object sender, EventArgs e)
        {
            var vm = new GoalViewModel(tappedGoalItem);
            vm.RemoveGoal.Execute(tappedGoalItem);
            Background_tapped(sender, e);
        }
    }
}