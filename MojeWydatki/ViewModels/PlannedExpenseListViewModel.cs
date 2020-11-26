using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.ViewModels
{
   public class PlannedExpenseListViewModel
    {
        PlannedExpenseRepository PlannedexpRep;
        public ObservableCollection<PlannedExpense> PlannedList;

        public PlannedExpenseListViewModel()
        {
            PlannedexpRep = new PlannedExpenseRepository();
        }
        public async Task MakePlannedExpenseList()
        {
            PlannedList = new ObservableCollection<PlannedExpense>();
            var iList = await PlannedexpRep.GetPlannedExpsAsync();

            foreach (PlannedExpense i in iList)
            {
                PlannedList.Add(i);
            }
        }
    }
}
