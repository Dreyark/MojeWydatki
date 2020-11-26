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
    public partial class AddPlannedExpenseView : ContentPage
    {
        public AddPlannedExpenseView()
        {
            InitializeComponent();
            StartDate.MinimumDate = DateTime.Now;
            EndDate.MinimumDate = DateTime.Now.AddDays(1);
        }
        void Value_Changed(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            bool is2 = Regex.IsMatch(Value.Text, @"^[0-9]+(\.[0-9]{0,2})?$|^$");
            if (!is2)
            {

                Value.Text = e.OldTextValue;

            }
        }

        public void OnSaveButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void StartDate_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
          EndDate.MinimumDate = StartDate.Date.AddDays(1);
        }

        private void EndDate_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(EndDate.Date<StartDate.Date)
            {
                EndDate.Date = StartDate.Date.AddDays(1);
            }
        }
    }
}