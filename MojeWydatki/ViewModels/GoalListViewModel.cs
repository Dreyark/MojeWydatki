using MojeWydatki.Data;
using MojeWydatki.Models;
using MojeWydatki.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class GoalListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Goal> GoalList { get; set; }

        GoalRepository goalRep;

        public GoalListViewModel(GoalListView activity)
        {
            goalRep = new GoalRepository();

            GetDataGoal = new Command(async () =>
            {
                GoalList = new ObservableCollection<Goal>();

                var iList = await goalRep.GetGoalsAsync();

                foreach (Goal i in iList)
                {
                    GoalList.Add(i);
                    activity.listView.ItemsSource = GoalList;
                }

            });
            RemoveGoal = new Command<Goal>(async (Goals) =>
            {
                await goalRep.DeleteGoalAsync(Goals);
                GoalList.Remove(Goals);
                System.Diagnostics.Debug.WriteLine("Data Remove : " + Goals.Title);
            });
        }

        public Command GetDataGoal { get; }

        public Command<Goal> RemoveGoal { get; }
    }
}
