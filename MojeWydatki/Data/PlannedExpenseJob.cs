using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data
{
    class PlannedExpenseJob
    {
        PlannedExpenseRepository planExpRepository;
        ExpenseRepository expenseRepository;

        public PlannedExpenseJob()
        {
            planExpRepository = new PlannedExpenseRepository();
            expenseRepository = new ExpenseRepository();
            MakePlannedExpenseList();
        }

        void MakePlannedExpenseList()
        {
            var iList = planExpRepository.GetPlannedExpsAsync().Result;

            foreach (PlannedExpense i in iList)
            {
                if (i.Repeatability == 1 && i.NextExpenseDate <= DateTime.Now && i.NextExpenseDate <= i.EndDate)
                {
                    var exp = new Expense
                    {
                        CategoryId = i.CategoryId,
                        Description = i.Description,
                        Date = i.NextExpenseDate,
                        Value = i.Value
                    };
                    expenseRepository.SaveExpenseAsync(exp);
                    i.NextExpenseDate.AddDays(1);
                    planExpRepository.SavePlannedExpAsync(i);
                }
                else if (i.Repeatability == 2 && i.NextExpenseDate <= DateTime.Now && i.NextExpenseDate <= i.EndDate)
                {
                    var exp = new Expense
                    {
                        CategoryId = i.CategoryId,
                        Description = i.Description,
                        Date = i.NextExpenseDate,
                        Value = i.Value
                    };
                    expenseRepository.SaveExpenseAsync(exp);
                    i.NextExpenseDate.AddDays(7);
                    planExpRepository.SavePlannedExpAsync(i);
                }
                else if (i.Repeatability == 3 && i.NextExpenseDate <= DateTime.Now && i.NextExpenseDate <= i.EndDate)
                {
                    var exp = new Expense
                    {
                        CategoryId = i.CategoryId,
                        Description = i.Description,
                        Date = i.NextExpenseDate,
                        Value = i.Value
                    };
                    expenseRepository.SaveExpenseAsync(exp);
                    i.NextExpenseDate.AddMonths(1);
                    planExpRepository.SavePlannedExpAsync(i);
                }
            }
        }

    }
}
