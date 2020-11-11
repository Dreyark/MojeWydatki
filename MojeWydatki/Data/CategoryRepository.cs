using MojeWydatki.Data.Interface;
using MojeWydatki.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.Data
{
    class CategoryRepository : ICategoryRepository
    {
        readonly SQLiteAsyncConnection _database;
        public CategoryRepository()
        {
            _database = new SQLiteAsyncConnection(App.DbPath);
            _database.CreateTableAsync<Category>().Wait();
        }
        public Task<List<Category>> GetCategoriesAsync()
        {
            return _database.Table<Category>().ToListAsync();
        }

        public Task SaveCategoryAsync(Category category)
        {
            var z = _database.Table<Category>().Where(i => i.CategoryTitle == category.CategoryTitle).CountAsync();
            if (z.Result == 0)
            {
                if (category.ID != 0)
                {
                    return _database.UpdateAsync(category);
                }
                else
                {
                    return _database.InsertAsync(category);
                }
            }

            return Task.FromResult(0);
        }
    }
}
