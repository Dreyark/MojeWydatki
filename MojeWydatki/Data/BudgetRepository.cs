using MojeWydatki.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data.Interface
{
    public class BudgetRepository : IBudgetRepository
    {
        readonly SQLiteAsyncConnection _database;
        public BudgetRepository()
        {
            _database = new SQLiteAsyncConnection(App.DbPath);
            _database.CreateTableAsync<Budget>().Wait();
        }
        public Task SaveBudgetAsync(Budget budget)
        {
            var z = _database.Table<Budget>().Where(i => i.Date == budget.Date).CountAsync();
            if (budget.ID != 0 || z.Result != 0)
            {
                budget.ID = _database.Table<Budget>().Where(i => i.Date == budget.Date).FirstAsync().Result.ID;
                return _database.UpdateAsync(budget);
            }
            else
            {
                return _database.InsertAsync(budget);
            }
        }
        public Task GetBudgetAsync(DateTime date)
        {
            return _database.Table<Budget>().Where(i => i.Date == date).FirstAsync();
        }

        public Task<List<Budget>> GetBudgetsAsync()
        {
            return _database.Table<Budget>().ToListAsync();
        }
    }
}
