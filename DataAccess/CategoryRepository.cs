using Dapper;
using Domain;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private IDbConnection _db;

        public CategoryRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public Category Add(Category category)
        {
            var insertCategorySql =
@"
INSERT INTO dbo.Categories (Name, Description)
VALUES (@Name, @Description);

SELECT SCOPE_IDENTITY();
";
            var id = _db.Query<int>(insertCategorySql, category).Single();
            category.Id = id;
            return category;
        }

        public Product Add(Product product)
        {
            var insertProductSql =
@"
INSERT INTO dbo.Products (Name, Price, Description, CategoryId)
VALUES (@Name, @Price, @Description, @CategoryId);

SELECT SCOPE_IDENTITY();
";

            var id = _db.Query<int>(insertProductSql, 
                new 
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryId = product.Category.Id
                }).Single();
            product.Id = id;
            return product;                
        }

        public Category Get(int id)
        {
            return _db.Query<Category>("SELECT Id, Name, Description FROM dbo.Categories WHERE Id = @Id", new { Id = id }).SingleOrDefault();
        }

        public List<Category> GetAll()
        {
            return _db.Query<Category>("SELECT Id, Name, Description FROM dbo.Categories").ToList();
        }

        public void Remove(int id)
        {
            _db.Execute("DELETE FROM dbo.Categories WHERE Id = @Id", new { Id = id });
        }

        public Category Modify(Category category)
        {
            var updateCategorySql =
@"
UPDATE dbo.Categories
SET
    Name = @Name
    , Description = @Description
WHERE   
    Id = @Id;
";

            _db.Execute(updateCategorySql, category);

            return category;
        }

        public Product Modify(Product product)
        {
            var updateProductSql =
@"
UPDATE dbo.Product
SET
    Name = @Name
    , Price = @Price
    , Description = @Description
    , CategoryId = @CategoryId;
";

            _db.Execute(updateProductSql, new
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.Category.Id
            });

            return product;
        }

        public Category GetWithProducts(int id)
        {
            var selectCategoryWithProducts =
@"
SELECT Id, Name, Description FROM dbo.Categories WHERE Id = @Id;
SELECT Id, Name, Price, Description, CategoryId FROM dbo.Products WHERE CategoryId = @Id;
";

            using (var multipleResults = _db.QueryMultiple(selectCategoryWithProducts, new { Id = id}))
            {
                var category = multipleResults.Read<Category>().SingleOrDefault();
                var products = multipleResults.Read<Product>().ToList();
                if (category != null && products != null)
                {
                    category.Products.AddRange(products);
                }

                return category;
            }
        }

        public void Save(Category category)
        {
            // todo: handle IsDeleted for Products
            if (category != null)
            {
                using (var txScope = new TransactionScope())
                {

                    if (category.IsNew)
                    {
                        Add(category);

                        if (category.Products != null)
                        {
                            foreach (var product in category.Products)
                            {
                                product.Category = category;
                                Add(product);
                            }
                        }
                    }
                    else
                    {
                        Modify(category);

                        if (category.Products != null)
                        {
                            foreach (var product in category.Products)
                            {
                                product.Category = category;
                                if (product != null)
                                {
                                    if (product.IsNew)
                                    {
                                        Add(product);
                                    }
                                    else
                                    {
                                        Modify(product);
                                    }
                                }
                            }
                        }
                    }

                    txScope.Complete();
                }
            }
        }
    }
}
