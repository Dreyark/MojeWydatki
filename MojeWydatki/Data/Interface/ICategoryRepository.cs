using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data.Interface
{
    public interface ICategoryRepository
    {
        public Task SaveCategoryAsync(Category category);

        public Task<List<Category>> GetCategoriesAsync();
    }
}
