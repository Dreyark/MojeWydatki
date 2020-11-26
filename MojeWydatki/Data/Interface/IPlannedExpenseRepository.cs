using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data.Interface
{
    public interface IPlannedExpenseRepository
    {
        public Task<List<PlannedExpense>> GetPlannedExpsAsync();
        public Task GetPlannedExpAsync(int id);
        public Task SavePlannedExpAsync(PlannedExpense plannedExpense);
        public Task DeletePlannedExpAsync(PlannedExpense plannedExpense);
    }
}
