namespace Models
{
    public class Category
    {
        public int CategoryId { get; }
        public string Name { get; }
        public string Description { get; }

        public Category(int categoryId, string categoryName, string categoryDescription)
        {
            CategoryId = categoryId;
            Name = categoryName;
            Description = categoryDescription;
        }

        public Category(int categoryId)
        {
            CategoryId = categoryId;
        }

        public Category(string categoryName, string description)
        {
            Name = categoryName;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
