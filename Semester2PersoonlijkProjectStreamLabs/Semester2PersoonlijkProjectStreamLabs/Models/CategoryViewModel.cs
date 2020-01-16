using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter a name for the category.")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please enter a description for the category.")]
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
