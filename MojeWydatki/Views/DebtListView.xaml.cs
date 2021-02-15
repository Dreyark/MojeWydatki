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

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DebtListView : TabbedPage
    {
        public DebtListView()
        {
            BindingContext = new DebtListViewModel();
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var vm = BindingContext as DebtListViewModel;
            CompletelistView.ItemsSource = null;
            InProgresslistView.ItemsSource = null;
            await vm.MakeDebtList();
            CompletelistView.ItemsSource = vm.DebtList.Where(i => i.Debt.Progress >=1);
            InProgresslistView.ItemsSource = vm.DebtList.Where(i => i.Debt.Progress < 1);
            base.OnAppearing();
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListDebt tappedDebtItem = e.Item as ListDebt;

            var DebtViewModelVM = new DebtViewModel(tappedDebtItem.Debt);
            var DebtPopupMenu = new UpdateDebtPopup(tappedDebtItem.Debt);
            DebtPopupMenu.CallbackEvent += (object sender, object e) => CallbackMethod();
            DebtPopupMenu.BindingContext = DebtViewModelVM;

            await PopupNavigation.Instance.PushAsync(DebtPopupMenu);
        }
        private void CallbackMethod()
        {
            OnAppearing();
        }

        private bool isOpen = false;
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                isOpen = true;
                FloatMenuItem1.IsVisible = true;
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);

                FloatMenuItem2.IsVisible = true;
                await FloatMenuItem2.TranslateTo(0, 0, 100);
                await FloatMenuItem2.TranslateTo(0, -20, 100);
                await FloatMenuItem2.TranslateTo(0, 0, 200);

            }
            else
            {
                isOpen = false;
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);
                FloatMenuItem1.IsVisible = false;

                await FloatMenuItem2.TranslateTo(0, 0, 100);
                await FloatMenuItem2.TranslateTo(0, -20, 100);
                await FloatMenuItem2.TranslateTo(0, 0, 200);
                FloatMenuItem2.IsVisible = false;
            }

        }

        private async void FloatMenuItem1Tap_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDebtView(false));
        }

        private async void FloatMenuItem2Tap_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDebtView(true));
        }
    }
}