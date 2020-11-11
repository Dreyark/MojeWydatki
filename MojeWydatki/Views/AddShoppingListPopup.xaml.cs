using MojeWydatki.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class AddShoppingListPopup : PopupPage
    {
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, EventArgs.Empty);
        public AddShoppingListPopup()
        {
            InitializeComponent();
        }

        async private void AddShoppingList_Clicked(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        private async void Background_tapped(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        private async Task CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}