using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data.Interface
{
    public interface IDebtRepository
    {
        public Task<List<Debt>> GetDebtsAsync();
        public Task GetDebtAsync(int id);
        public Task SaveDebtAsync(Debt debt);
        public Task DeleteDebtAsync(Debt debt);
    }
}
