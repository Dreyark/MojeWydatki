using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    class CategoryViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        CategoryRepository catRep;
        public CategoryViewModel()
        {
            catRep = new CategoryRepository();

            SaveCategoryCommand = new Command(async () =>
            {
                var category = new Category();
                category.CategoryTitle = TheCategoryTitle;
                await catRep.SaveCategoryAsync(category);
            });
        }

        string categoryTitle;
        public string TheCategoryTitle
        {
            get => categoryTitle;
            set
            {
                categoryTitle = value;
                var args = new PropertyChangedEventArgs(nameof(TheCategoryTitle));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command SaveCategoryCommand { get; }
    }
}
