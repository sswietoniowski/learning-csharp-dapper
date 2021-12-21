namespace Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool IsNew => Id == default(int);
        public List<Product> Products { get; } = new List<Product>();
    }
}
