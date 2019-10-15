using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface ICategoryContext
    {
        List<Category> GetAllCategories();
        void AddNewCategory(Category newCategory);
        void EditCategory(Category category);
        void DeleteCategory(Category category);
    }
}
