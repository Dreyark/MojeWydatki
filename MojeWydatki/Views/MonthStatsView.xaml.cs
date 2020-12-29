using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using MojeWydatki.ViewModels;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthStatsView : ContentPage
    {

        List<Microcharts.ChartEntry> entries;
        MonthStatsViewModel monthStatsViewModel;
        public List<String> DateList;
        public int SelectedDate;
        public int NewSelectedDate;
        public MonthStatsView()
        {
            SelectedDate = 1;
            InitializeComponent();
            Chart(DateTime.Now);
        }

        public void Chart(DateTime date)
        {
            SelectedDate = NewSelectedDate;
            entries = new List<Microcharts.ChartEntry>();
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            Random rnd = new Random();
            monthStatsViewModel = new MonthStatsViewModel();
            monthStatsViewModel.ExpensesList();
            monthStatsViewModel.MakeStatsList(date);
            if (monthStatsViewModel.categoryChart.Count() != 0)
            {
                foreach (var x in monthStatsViewModel.categoryChart)
            {
                var col = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)));
                entries.Add(
                                new ChartEntry((float)x.Value)
                                {
                                    Color = col,
                                    Label = x.Category,
                                    ValueLabelColor = col,
                                    ValueLabel = Convert.ToString(x.Value),
                                });
            }
                Chart1.Chart = new DonutChart { Entries = entries };
                Chart1.Chart.LabelTextSize = 35;
                listView.ItemsSource = monthStatsViewModel.ExtendedExpenseList.Where(i => i.Expense.Date > firstDayOfMonth && i.Expense.Date < lastDayOfMonth).OrderByDescending(i => i.Expense.Value);

            }
            else
            {
                notification.IsEnabled = true;
                notification.IsVisible = true;
                notification.Text = "Za mała ilość danych";
                Chart1.IsEnabled = false;
                listView.IsEnabled = false;
            }
        }

        public async void ChangeDateClicked(object sender, EventArgs e)
        {
            DateList = new List<String>();

            foreach (var i in monthStatsViewModel.dateList)
            {
                DateList.Add(i.ToString("MMMM yyyy"));
            }
            var monthStatsPopup = new MonthStatsPopup(this);
            monthStatsPopup.CallbackEvent += (object sender, object e) => CallbackMethod();
            await PopupNavigation.Instance.PushAsync(monthStatsPopup);
        }

        private void CallbackMethod()
        {
            if (SelectedDate != NewSelectedDate)
            {
                Chart(monthStatsViewModel.dateList[NewSelectedDate]);
            }
        }
    }
}