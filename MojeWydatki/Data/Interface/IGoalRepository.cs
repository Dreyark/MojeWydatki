using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data.Interface
{
    public interface IGoalRepository
    {
        public Task<List<Goal>> GetGoalsAsync();
        public Task GetGoalAsync(int id);
        public Task SaveGoalAsync(Goal goal);
        public Task DeleteGoalAsync(Goal goal);
    }
}
