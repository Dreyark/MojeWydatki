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
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            var vm = new HomeViewModel();
            vm.setBalance();
            InitializeComponent();
            BalanceLabel.Text = Convert.ToString(vm.MonthBalance);
        }

        async private void AddCategory_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddCategoryPopup());
        }

        async private void SetBudget_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SetBudgetPopup());
        }
    }
}