using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface ICategoryContext
    {
        List<Category> GetAllCategories();
        void AddNewCategory(Category newCategory);
        void EditCategory(Category category);
        void DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
    }
}
