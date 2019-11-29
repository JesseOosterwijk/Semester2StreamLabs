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

        public Category(string categoryName)
        {
            Name = categoryName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
