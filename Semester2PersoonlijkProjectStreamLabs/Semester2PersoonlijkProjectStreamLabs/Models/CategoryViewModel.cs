using Models;
using System.Collections.Generic;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; internal set; }

        public CategoryViewModel()
        {
            ;
        }

        public CategoryViewModel(int categoryId, string categoryName, string description)
        {
            CategoryId = categoryId;
            CategoryName = CategoryName;
            Description = description;
        }

        public CategoryViewModel(string categoryName, string description)
        {
            CategoryName = CategoryName;
            Description = description;
        }

        public CategoryViewModel(Category category)
        {
            CategoryId = category.CategoryId;
            CategoryName = category.Name;
            Description = category.Description;
        }
    }
}
