using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using MojeWydatki.ViewModels;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthStatsView : ContentPage
    {

        List<Microcharts.ChartEntry> entries = new List<Microcharts.ChartEntry>();
        public MonthStatsView()
        {
            Random rnd = new Random();
            var vm = new MonthStatsViewModel();
            vm.MakeStatsList();
            foreach (var x in vm.categoryChart)
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
            InitializeComponent();
            Chart1.Chart = new DonutChart { Entries = entries };
            Chart1.Chart.LabelTextSize = 35;
            vm.MostExpensiveExpenses();
            listView.ItemsSource = vm.BiggestExpensesList;
        }

    }
}