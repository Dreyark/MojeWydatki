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
    public partial class SetBudgetPopup : PopupPage
    {
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, EventArgs.Empty);
        public SetBudgetPopup()
        {
            InitializeComponent();
        }
        private async void Background_tapped(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        async private void SetBudget_Clicked(object sender, EventArgs e)
        {

            await CloseAllPopup();
        }

        private async Task CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        void Entry_BudgetValueChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            bool is2 = Regex.IsMatch(SetBudget.Text, @"^[0-9]+(\.[0-9]{0,2})?$|^$");
            if (!is2)
            {

                SetBudget.Text = e.OldTextValue;

            }
            if (newText == "")
            {

                SetBudgetButton.IsVisible = false;

            }
            else
            {
                SetBudgetButton.IsVisible = true;
            }
        }
    }
}