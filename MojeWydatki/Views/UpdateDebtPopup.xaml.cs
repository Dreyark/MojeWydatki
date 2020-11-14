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
    public partial class UpdateDebtPopup : PopupPage
    {
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, EventArgs.Empty);
        Debt tappedDebtItem;
        public UpdateDebtPopup(Debt debt)
        {
            tappedDebtItem = debt;
            InitializeComponent();
            SetVisible();
        }
        private void SetVisible()
        {
            if (tappedDebtItem.Progress >= 1)
            {
                CurrentValueEntry.IsVisible = false;
                UpdateDebtButton.IsVisible = false;
            }
        }

        private async void Background_tapped(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        async private void UpdateDebt_Clicked(object sender, EventArgs e)
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
            bool is2 = Regex.IsMatch(CurrentValueEntry.Text, @"^[0-9]+(\.[0-9]{0,2})?$|^$");
            if (!is2)
            {

                CurrentValueEntry.Text = e.OldTextValue;

            }
            if (newText == "")
            {

                UpdateDebtButton.IsVisible = false;

            }
            else
            {
                UpdateDebtButton.IsVisible = true;
            }
        }

        private void RemoveDebt_Clicked(object sender, EventArgs e)
        {
            var vm = new DebtViewModel(tappedDebtItem);
            vm.RemoveDebt.Execute(tappedDebtItem);
            Background_tapped(sender, e);
        }
    }
}