using MojeWydatki.Data;
using MojeWydatki.Models;
using MojeWydatki.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class GoalListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Goal> GoalList { get; set; }

        GoalRepository goalRep;

        public GoalListViewModel()
        {
            goalRep = new GoalRepository();
        }

        public async Task MakeGoalList()
        {
            GoalList = new ObservableCollection<Goal>();
            var iList = await goalRep.GetGoalsAsync();

            foreach (Goal i in iList)
            {
                GoalList.Add(i);
            }
        }
    }
}
