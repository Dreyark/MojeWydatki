using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class GoalViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public String minimumDate = "11-03-2020";

        GoalRepository goalRep;

        public Double progress;

        public GoalViewModel()
        {
            goalRep = new GoalRepository();

            SaveGoalCommand = new Command(async () =>
            {
                var goal = new Goal();
                goal.Title = TheTitle;
                goal.CurrentValue = TheCurrentValue;
                goal.GoalValue = TheGoalValue;
                goal.StartDate = DateTime.Now;
                goal.EndDate = TheEndDate;
                goal.Progress = TheCurrentValue / TheGoalValue;
                await goalRep.SaveGoalAsync(goal);
            });
        }

        public GoalViewModel(Goal goals)
        {
            goalRep = new GoalRepository();

            TheTitle = goals.Title;
            TheCurrentValue = goals.CurrentValue;
            TheGoalValue = goals.GoalValue;
            TheEndDate = goals.EndDate;
            TheIsFinished = goals.IsFinished;
            progress = goals.Progress;

            SaveGoalCommand = new Command(async () =>
            {
                goals.Title = TheTitle;
                goals.CurrentValue = TheCurrentValue;
                goals.GoalValue = TheGoalValue;
                goals.StartDate = DateTime.Now;
                goals.EndDate = TheEndDate;
                goals.Progress = TheCurrentValue / TheGoalValue;
                await goalRep.SaveGoalAsync(goals);
                TheTitle = string.Empty;
                TheCurrentValue = 0;
                TheGoalValue = 0;
                TheIsFinished = false;
                TheEndDate = DateTime.MinValue;
                progress = 0;
            });
        }

        string title;
        public string TheTitle
        {
            get => title;
            set
            {
                title = value;
                var args = new PropertyChangedEventArgs(nameof(TheTitle));
                PropertyChanged?.Invoke(this, args);
            }
        }

        double currentValue;
        public double TheCurrentValue
        {
            get => currentValue;
            set
            {
                this.currentValue = value;
                var args = new PropertyChangedEventArgs(nameof(TheCurrentValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        double goalValue;
        public double TheGoalValue
        {
            get => goalValue;
            set
            {
                this.goalValue = value;
                var args = new PropertyChangedEventArgs(nameof(TheGoalValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        DateTime endDate;
        public DateTime TheEndDate
        {
            get => endDate;
            set
            {
                this.endDate = value;
                var args = new PropertyChangedEventArgs(nameof(TheEndDate));
                PropertyChanged?.Invoke(this, args);
            }
        }

        bool isFinished;
        public bool TheIsFinished
        {
            get => isFinished;
            set
            {
                this.isFinished = value;
                var args = new PropertyChangedEventArgs(nameof(TheIsFinished));
                PropertyChanged?.Invoke(this, args);
            }
        }


        public Command SaveGoalCommand { get; }
        public Goal BindingContext { get; }
    }
}
