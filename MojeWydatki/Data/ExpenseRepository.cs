using MojeWydatki.Models;
using MojeWydatki.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data
{
    class ExpenseRepository : IExpenseRepository
    {
        readonly SQLiteAsyncConnection _database;
        public ExpenseRepository()
        {
            _database = new SQLiteAsyncConnection(App.DbPath);
            _database.CreateTableAsync<Expense>().Wait();
        }

        public Task DeleteExpenseAsync(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }

        public Task GetExpenseAsync(int id)
        {
            return _database.Table<Expense>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<Expense>> GetExpensesAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task SaveExpenseAsync(Expense expense)
        {
            if (expense.ID != 0)
            {
                return _database.UpdateAsync(expense);
            }
            else
            {
                return _database.InsertAsync(expense);
            }
        }
    }
}
