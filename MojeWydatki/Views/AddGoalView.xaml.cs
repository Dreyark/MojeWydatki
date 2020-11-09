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
    public partial class AddGoalView : ContentPage
    {
        public AddGoalView()
        {
            InitializeComponent();
            EndDate.MinimumDate = DateTime.Now;
        }
        void CurrentValue_Changed(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            bool is2 = Regex.IsMatch(CurrentValue.Text, @"^[0-9]+(\.[0-9]{0,2})?$|^$");
            if (!is2)
            {

                CurrentValue.Text = e.OldTextValue;

            }
        }

        void GoalValue_Changed(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            bool is2 = Regex.IsMatch(GoalValue.Text, @"^[0-9]+(\.[0-9]{0,2})?$|^$");
            if (!is2)
            {

                GoalValue.Text = e.OldTextValue;

            }
        }
    }
}