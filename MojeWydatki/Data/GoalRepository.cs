using MojeWydatki.Data.Interface;
using MojeWydatki.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data
{
    class GoalRepository : IGoalRepository
    {
        readonly SQLiteAsyncConnection _database;
        public GoalRepository()
        {
            _database = new SQLiteAsyncConnection(App.DbPath);
            _database.CreateTableAsync<Goal>().Wait();
        }
        public Task DeleteGoalAsync(Goal goal)
        {
            return _database.DeleteAsync(goal);
        }

        public Task GetGoalAsync(int id)
        {
            return _database.Table<Goal>()
                             .Where(i => i.ID == id)
                             .FirstOrDefaultAsync();
        }

        public Task<List<Goal>> GetGoalsAsync()
        {
            return _database.Table<Goal>().ToListAsync();
        }

        public Task SaveGoalAsync(Goal goal)
        {
            if (goal.ID != 0)
            {
                return _database.UpdateAsync(goal);
            }
            else
            {
                return _database.InsertAsync(goal);
            }
        }
    }
}
