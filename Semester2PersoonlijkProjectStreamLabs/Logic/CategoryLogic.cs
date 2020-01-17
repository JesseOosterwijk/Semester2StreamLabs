using System.Collections.Generic;
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

        public void DeleteCategory(int categoryId)
        {
            _category.DeleteCategory(categoryId);
        }

        public Category GetCategoryById(int categoryId)
        {
            return _category.GetCategoryById(categoryId);
        }
    }
}
