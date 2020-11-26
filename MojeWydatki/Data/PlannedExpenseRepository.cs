using MojeWydatki.Data.Interface;
using MojeWydatki.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data
{
    public class PlannedExpenseRepository : IPlannedExpenseRepository
    {
        readonly SQLiteAsyncConnection _database;
        public PlannedExpenseRepository()
        {
            _database = new SQLiteAsyncConnection(App.DbPath);
            _database.CreateTableAsync<PlannedExpense>().Wait();
        }
        public Task DeletePlannedExpAsync(PlannedExpense plannedExpense)
        {
            return _database.Table<PlannedExpense>().ToListAsync();
        }

        public Task<List<PlannedExpense>> GetPlannedExpsAsync()
        {
            return _database.Table<PlannedExpense>().ToListAsync();
        }

        public Task GetPlannedExpAsync(int id)
        {
            return _database.Table<PlannedExpense>()
                 .Where(i => i.ID == id)
                 .FirstOrDefaultAsync();
        }

        public Task SavePlannedExpAsync(PlannedExpense plannedExpense)
        {
            if (plannedExpense.ID != 0)
            {
                return _database.UpdateAsync(plannedExpense);
            }
            else
            {
                return _database.InsertAsync(plannedExpense);
            }
        }
    }
}
