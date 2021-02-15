using MojeWydatki.ViewModels;
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
    public partial class AddDebtView : ContentPage
    {
        public AddDebtView(bool amILender)
        {
            InitializeComponent();
            var debtViewModel = new DebtViewModel();
            BindingContext = debtViewModel;
            debtViewModel.TheAmILender = amILender;
            if (amILender)
            {
                LendFrom.Text = "Komu pożyczyłem";
            }
            else
            {
                LendFrom.Text = "Pożyczyłem od";
            }
            BorrowDate.Date = DateTime.Now;
            DateOfDelivery.MinimumDate = DateTime.Now.AddDays(1);
        }

        public void OnSaveButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void StartDate_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DateOfDelivery.MinimumDate = BorrowDate.Date.AddDays(1);
        }

        void DebtValue_Changed(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            if (e.NewTextValue == null)
            {

            }
            else
            {
                bool is2 = Regex.IsMatch(DebtValue.Text, @"^[0-9]+((\.|\,)[0-9]{0,2})?$|^$");
                if (!is2)
                {

                    DebtValue.Text = e.OldTextValue;

                }
            }
        }
    }
}