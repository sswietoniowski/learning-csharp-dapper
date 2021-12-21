using Dapper.Contrib.Extensions;

namespace Domain
{
    [Table("dbo.Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Computed]
        public bool IsNew => Id == default(int);
        [Write(false)]
        public List<Product> Products { get; } = new List<Product>();
    }
}
