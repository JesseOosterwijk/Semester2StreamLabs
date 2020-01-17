using Data.Interfaces;
using Models;
using System.Collections.Generic;

namespace Data.Memory
{
    public class CategoryMemory : ICategoryContext
    {
        List<Category> CategoryList = new List<Category>();

        public CategoryMemory(List<Category> categories)
        {
            CategoryList = categories;
        }
        public List<Category> GetAllCategories()
        {
            return CategoryList;
        }

        public void AddNewCategory(Category category)
        {
            CategoryList.Add(category);
        }

        public void EditCategory(Category category)
        {

        }

        public Category GetCategoryById(int categoryId)
        {
            return new Category(1, "foo", "baa");
        }

        public void DeleteCategory(int categoryId)
        {

        }
    }
}
