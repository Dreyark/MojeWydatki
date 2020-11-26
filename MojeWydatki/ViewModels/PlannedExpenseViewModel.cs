using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class PlannedExpenseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<Category> CategoryList { get; set; }
        public IList<String> Repeatability { get; set; }

        PlannedExpenseRepository PlannedexpRep;
        CategoryRepository catRep;
        public PlannedExpenseViewModel()
        {
            PlannedexpRep = new PlannedExpenseRepository();
            catRep = new CategoryRepository();
            CategoryList = new ObservableCollection<Category>();
            Repeatability = new ObservableCollection<String>();

            var iList = catRep.GetCategoriesAsync();
            foreach (Category i in iList.Result)
            {
                CategoryList.Add(i);
            }
            Repeatability.Add("Codziennie");
            Repeatability.Add("Co tydzień");
            Repeatability.Add("Co miesiąc");

            SavePlannedExpenseCommand = new Command(async () =>
            {
                var plannedExpense = new PlannedExpense();
                plannedExpense.Description = TheDescription;
                plannedExpense.Value = Convert.ToDouble(TheValue);
                plannedExpense.StartDate = TheStartDate;
                plannedExpense.NextExpenseDate = TheStartDate;
                plannedExpense.EndDate = TheEndDate;
                plannedExpense.CategoryId = CategoryId + 1;
                plannedExpense.Repeatability = RepeatabilityId + 1;
                await PlannedexpRep.SavePlannedExpAsync(plannedExpense);
                TheDescription = string.Empty;
                TheValue = "0";
                CategoryId = -1;
            });
        }

        public PlannedExpenseViewModel(PlannedExpense Plexpense)
        {
            PlannedexpRep = new PlannedExpenseRepository();
            catRep = new CategoryRepository();
            CategoryList = new ObservableCollection<Category>();
            Repeatability = new ObservableCollection<String>();

            var iList = catRep.GetCategoriesAsync();
            foreach (Category i in iList.Result)
            {
                CategoryList.Add(i);
            }

            Repeatability.Add("Codziennie");
            Repeatability.Add("Co tydzień");
            Repeatability.Add("Co miesiąc");
            var Id = Plexpense.ID;
            TheStartDate = Plexpense.StartDate;
            TheEndDate = Plexpense.EndDate;
            TheDescription = Plexpense.Description;
            TheValue = Convert.ToString(Plexpense.Value);
            CategoryId = Plexpense.CategoryId - 1;
            RepeatabilityId = Plexpense.Repeatability - 1;

            SavePlannedExpenseCommand = new Command(async () =>
            {
                Plexpense.ID = Id;
                Plexpense.Description = TheDescription;
                Plexpense.Value = Convert.ToDouble(TheValue);
                Plexpense.StartDate = TheStartDate;
                Plexpense.EndDate = TheEndDate;
                Plexpense.NextExpenseDate = TheStartDate;
                Plexpense.CategoryId = CategoryId + 1;
                Plexpense.Repeatability = RepeatabilityId + 1;
                await PlannedexpRep.SavePlannedExpAsync(Plexpense);
                TheDescription = string.Empty;
                TheValue = string.Empty;
                CategoryId = -1;
            });

            RemovePlannedExpense = new Command(async () =>
            {
                await PlannedexpRep.DeletePlannedExpAsync(Plexpense);
            });
        }

        string description;
        public string TheDescription
        {
            get => description;
            set
            {
                description = value;
                var args = new PropertyChangedEventArgs(nameof(TheDescription));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string category;
        public string TheCategory
        {
            get => category;
            set
            {
                category = value;
                var args = new PropertyChangedEventArgs(nameof(TheCategory));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string value;
        public string TheValue
        {
            get => value;
            set
            {
                this.value = value;
                if (this.value == "")
                {
                    this.value = "0";
                }
                else
                {
                    if (this.value.Last() == '.')
                    {
                        this.value = this.value.Remove(this.value.Length - 1);
                    }
                }

                var args = new PropertyChangedEventArgs(nameof(TheValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        int categoryId;
        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                this.categoryId = value;
                var args = new PropertyChangedEventArgs(nameof(CategoryList));
                PropertyChanged?.Invoke(this, args);
            }
        }

        int repeatabilityId;
        public int RepeatabilityId
        {
            get
            {
                return repeatabilityId;
            }
            set
            {
                this.repeatabilityId = value;
                var args = new PropertyChangedEventArgs(nameof(RepeatabilityId));
                PropertyChanged?.Invoke(this, args);
            }
        }

        DateTime startDate;
        public DateTime TheStartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                this.startDate = value;
                var args = new PropertyChangedEventArgs(nameof(TheStartDate));
                PropertyChanged?.Invoke(this, args);
            }
        }

        DateTime endDate;
        public DateTime TheEndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                this.endDate = value;
                var args = new PropertyChangedEventArgs(nameof(TheEndDate));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command RemovePlannedExpense { get; }
        public Command SavePlannedExpenseCommand { get; }
    }
}
