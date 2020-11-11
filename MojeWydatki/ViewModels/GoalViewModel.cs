using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class GoalViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        GoalRepository goalRep;

        public Double progress;

        public GoalViewModel()
        {
            goalRep = new GoalRepository();

            SaveGoalCommand = new Command(async () =>
            {
                var goal = new Goal();
                goal.Title = TheTitle;
                goal.CurrentValue = Convert.ToDouble(TheCurrentValue);
                goal.GoalValue = Convert.ToDouble(TheGoalValue);
                goal.StartDate = DateTime.Now;
                goal.EndDate = TheEndDate;
                goal.Progress = goal.CurrentValue / goal.GoalValue;
                goal.IsFinished = false;
                await goalRep.SaveGoalAsync(goal);
            });
        }

        public GoalViewModel(Goal goal)
        {
            goalRep = new GoalRepository();

            TheTitle = goal.Title;
            TheCurrentValue = Convert.ToString(goal.CurrentValue);
            TheGoalValue = Convert.ToString(goal.GoalValue);
            TheEndDate = goal.EndDate;
            TheIsFinished = goal.IsFinished;
            progress = goal.Progress;

            SaveGoalCommand = new Command(async () =>
            {
                goal.Title = TheTitle;
                goal.CurrentValue = Convert.ToDouble(TheCurrentValue);
                goal.GoalValue = Convert.ToDouble(TheGoalValue);
                goal.StartDate = DateTime.Now;
                goal.EndDate = TheEndDate;
                goal.Progress = goal.CurrentValue / goal.GoalValue;
                await goalRep.SaveGoalAsync(goal);
                TheTitle = string.Empty;
                TheCurrentValue = string.Empty;
                TheGoalValue = string.Empty;
                TheIsFinished = false;
                TheEndDate = DateTime.MinValue;
                progress = 0;
            });

            UpdateGoalCommand = new Command(async () =>
            {
                goal.Title = TheTitle;
                goal.CurrentValue = Convert.ToDouble(TheCurrentValue)+ Convert.ToDouble(TheAddValue);
                goal.GoalValue = Convert.ToDouble(TheGoalValue);
                goal.Progress = goal.CurrentValue / goal.GoalValue;
                if (goal.Progress >= 1) goal.IsFinished = true;
                await goalRep.SaveGoalAsync(goal);
                TheTitle = string.Empty;
                TheCurrentValue = string.Empty;
                TheGoalValue = string.Empty;
                TheIsFinished = false;
                TheEndDate = DateTime.MinValue;
                progress = 0;
            });

            RemoveGoal = new Command(async () =>
            {
                await goalRep.DeleteGoalAsync(goal);
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

        String currentValue;
        public String TheCurrentValue
        {
            get => currentValue;
            set
            {
                this.currentValue = value;
                if (this.currentValue == "")
                {
                    this.currentValue = "0";
                }
                else
                {
                    if (this.currentValue.Last() == '.')
                    {
                        this.currentValue = this.currentValue.Remove(this.currentValue.Length - 1);
                    }
                }
                var args = new PropertyChangedEventArgs(nameof(TheCurrentValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        String addValue;
        public String TheAddValue
        {
            get => addValue;
            set
            {
                this.addValue = value;
                if (this.addValue == "")
                {
                    this.addValue = "0";
                }
                else
                {
                    if (this.addValue.Last() == '.')
                    {
                        this.addValue = this.addValue.Remove(this.addValue.Length - 1);
                    }
                }
                var args = new PropertyChangedEventArgs(nameof(TheAddValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        String goalValue;
        public String TheGoalValue
        {
            get => goalValue;
            set
            {
                this.goalValue = value;
                if (this.TheGoalValue == "")
                {
                    this.TheGoalValue = "0";
                }
                else
                {
                    if (this.TheGoalValue.Last() == '.')
                    {
                        this.TheGoalValue = this.TheGoalValue.Remove(this.currentValue.Length - 1);
                    }
                }
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

        public Command UpdateGoalCommand { get; }
        public Command SaveGoalCommand { get; }
        public Command RemoveGoal { get; }
        public Goal BindingContext { get; }
    }
}
