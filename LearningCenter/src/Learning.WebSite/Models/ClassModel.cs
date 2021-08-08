namespace Learning.WebSite.Models
{
    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }

        public ClassModel(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
    }
}