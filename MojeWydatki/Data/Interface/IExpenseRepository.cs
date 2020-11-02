using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MojeWydatki.Services
{
    public interface IExpenseRepository
    {
        public Task<List<Expense>> GetExpensesAsync();
        public Task GetExpenseAsync(int id);
        public Task SaveExpenseAsync(Expense expense);
        public Task DeleteExpenseAsync(Expense expense);
    }
}
