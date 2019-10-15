using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class CategoryLogic
    {
        private readonly ICategoryContext _category;

        public CategoryLogic(ICategoryContext category)
        {
            _category = category;
        }

        public List<Category> GetAllCategories()
        {
            return _category.GetAllCategories();
        }

        public void AddNewCategory(Category newCategory)
        {
            _category.AddNewCategory(newCategory);
        }

        public void EditCategory(Category category)
        {
            _category.EditCategory(category);
        }

        public void DeleteCategory(Category category)
        {
            _category.DeleteCategory(category);
        }
    }
}
