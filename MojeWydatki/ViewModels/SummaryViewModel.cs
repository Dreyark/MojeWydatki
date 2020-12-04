using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.ViewModels
{
    class SummaryViewModel
    {
        public Summary summary;
        public SummaryViewModel()
        {
        }

        public void MakeSummaryList(DateTime date)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            summary = new Summary();
            summary = App.Database.ThreeMonthsStats(firstDayOfMonth, lastDayOfMonth);
        }
    }
}
