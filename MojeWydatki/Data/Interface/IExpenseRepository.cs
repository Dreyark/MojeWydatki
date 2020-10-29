using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MojeWydatki.Services
{
    public interface IExpenseRepository
    {
        Task Initialize();
        Task<List<Expense>> GetExpensesAsync();
        Task GetExpenseAsync(int id);
        Task SaveExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(Expense expense);
    }
}
