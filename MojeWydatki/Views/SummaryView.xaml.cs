using Microcharts;
using MojeWydatki.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryView : ContentPage
    {
        SummaryViewModel summaryViewModel;
        List<Microcharts.ChartEntry> entries = new List<Microcharts.ChartEntry>();
        public SummaryView()
        {
            summaryViewModel = new SummaryViewModel();
            InitializeComponent();
            Charts();
        }

        void Charts()
        {
            summaryViewModel.MakeSummaryList(DateTime.Now);
            for (var i = 1; i < summaryViewModel.summary.ValuePerDay.Length + 1; i++)
            {
                entries.Add(
                new ChartEntry((float)summaryViewModel.summary.ValuePerDay[i - 1])
                {
                    Color = SkiaSharp.SKColor.Parse("#3498db"),
                    Label = i.ToString(),
                    ValueLabelColor = SkiaSharp.SKColor.Parse("#3498db"),
                    ValueLabel = Convert.ToString(summaryViewModel.summary.ValuePerDay[i - 1]),
                });
            }
            Month1.Chart = new BarChart { Entries = entries };
            Month1.Chart.LabelTextSize = 29;
            var sub = summaryViewModel.summary.Budget - summaryViewModel.summary.Value;
            var perDay = Math.Round(summaryViewModel.summary.Value / summaryViewModel.summary.ValuePerDay.Length, 2);
            Month1Description.Text = String.Format("  {0,-10} {1,28} {2,28} ", "Budżet", "Wydano", "Zaoszczędzono");
            Month1Description21.Text = summaryViewModel.summary.Budget.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month1Description22.Text = summaryViewModel.summary.Value.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month1Description23.Text = sub.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month1Description3.Text = "Dziennie srednio wydano: " + perDay.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month1lab.Text = summaryViewModel.summary.Date.ToString("MMMM");
            Month1lab.FontSize = 20;
            Month1lab.FontAttributes = FontAttributes.Bold;
            Month1lab.TextColor = Xamarin.Forms.Color.FromHex("#3498db");
            entries = new List<Microcharts.ChartEntry>();
            summaryViewModel.MakeSummaryList(DateTime.Now.AddMonths(-1));
            for (var i = 1; i < summaryViewModel.summary.ValuePerDay.Length + 1; i++)
            {
                entries.Add(
                new ChartEntry((float)summaryViewModel.summary.ValuePerDay[i - 1])
                {
                    Color = SkiaSharp.SKColor.Parse("#77d065"),
                    Label = i.ToString(),
                    ValueLabelColor = SkiaSharp.SKColor.Parse("#77d065"),
                    ValueLabel = Convert.ToString(summaryViewModel.summary.ValuePerDay[i - 1]),
                });
            }
            Month2.Chart = new BarChart { Entries = entries };
            Month2.Chart.LabelTextSize = 29;
            sub = summaryViewModel.summary.Budget - summaryViewModel.summary.Value;
            perDay = Math.Round(summaryViewModel.summary.Value / summaryViewModel.summary.ValuePerDay.Length, 2);
            Month2Description.Text = String.Format("  {0,-12} {1,28} {2,28} ", "Budżet", "Wydano", "Zaoszczędzono");
            Month2Description21.Text = summaryViewModel.summary.Budget.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month2Description22.Text = summaryViewModel.summary.Value.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month2Description23.Text = sub.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month2Description3.Text = "Dziennie srednio wydano: " + perDay.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month2lab.Text = summaryViewModel.summary.Date.ToString("MMMM");
            Month2lab.FontSize = 20;
            Month2lab.FontAttributes = FontAttributes.Bold;
            Month2lab.TextColor = Xamarin.Forms.Color.FromHex("#77d065");
            entries = new List<Microcharts.ChartEntry>();
            summaryViewModel.MakeSummaryList(DateTime.Now.AddMonths(-2));
            for (var i = 1; i < summaryViewModel.summary.ValuePerDay.Length + 1; i++)
            {
                entries.Add(
                new ChartEntry((float)summaryViewModel.summary.ValuePerDay[i - 1])
                {
                    Color = SkiaSharp.SKColor.Parse("#b455b6"),
                    Label = i.ToString(),
                    ValueLabelColor = SkiaSharp.SKColor.Parse("#b455b6"),
                    ValueLabel = Convert.ToString(summaryViewModel.summary.ValuePerDay[i - 1]),
                });
            }
            Month3.Chart = new BarChart { Entries = entries };
            Month3.Chart.LabelTextSize = 29;
            sub = summaryViewModel.summary.Budget - summaryViewModel.summary.Value;
            perDay = Math.Round(summaryViewModel.summary.Value / summaryViewModel.summary.ValuePerDay.Length, 2);
            Month3Description.Text = String.Format("  {0,-12} {1,28} {2,28} ", "Budżet", "Wydano", "Zaoszczędzono");
            Month3Description21.Text = summaryViewModel.summary.Budget.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month3Description22.Text = summaryViewModel.summary.Value.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month3Description23.Text = sub.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month3Description3.Text = "Dziennie srednio wydano: " + perDay.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
            Month3lab.Text = summaryViewModel.summary.Date.ToString("MMMM");
            Month3lab.FontSize = 20;
            Month3lab.FontAttributes = FontAttributes.Bold;
            Month3lab.TextColor = Xamarin.Forms.Color.FromHex("#b455b6");
        }
    }
}