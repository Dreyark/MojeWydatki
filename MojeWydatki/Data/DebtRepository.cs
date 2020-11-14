using MojeWydatki.Data.Interface;
using MojeWydatki.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data
{
    public class DebtRepository : IDebtRepository
    {
        readonly SQLiteAsyncConnection _database;
        public DebtRepository()
        {
            _database = new SQLiteAsyncConnection(App.DbPath);
            _database.CreateTableAsync<Debt>().Wait();
        }

        public Task DeleteDebtAsync(Debt debt)
        {
            return _database.DeleteAsync(debt);
        }

        public Task GetDebtAsync(int id)
        {
            return _database.Table<Debt>()
                             .Where(i => i.ID == id)
                             .FirstOrDefaultAsync();
        }

        public Task<List<Debt>> GetDebtsAsync()
        {
            return _database.Table<Debt>().ToListAsync();
        }

        public Task SaveDebtAsync(Debt debt)
        {
            if (debt.ID != 0)
            {
                return _database.UpdateAsync(debt);
            }
            else
            {
                return _database.InsertAsync(debt);
            }
        }
    }
}
