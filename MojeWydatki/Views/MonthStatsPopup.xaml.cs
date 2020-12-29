using MojeWydatki.ViewModels;
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
    public partial class MonthStatsPopup : PopupPage
    {
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, EventArgs.Empty);

        MonthStatsView monthStatsView;
        public MonthStatsPopup(MonthStatsView vm)
        {
            monthStatsView = vm;
            InitializeComponent();
            DateEntry.ItemsSource = vm.DateList;
            DateEntry.Title = "Wybierz datę";
            DateEntry.SelectedIndex = vm.SelectedDate;
        }

        private async void Background_tapped(object sender, EventArgs e)
        {
            monthStatsView.NewSelectedDate = monthStatsView.SelectedDate;
            await CloseAllPopup();
        }

        private async Task CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void DateEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            monthStatsView.NewSelectedDate = DateEntry.SelectedIndex;
            await CloseAllPopup();
        }
    }
}