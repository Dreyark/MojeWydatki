using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data.Interface
{
    public interface IBudgetRepository
    {
        public Task SaveBudgetAsync(Budget budget);

        public Task GetBudgetAsync(DateTime date);
    }
}
