using Dapper;
using Domain;
using System.Data;
using System.Data.SqlClient;

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

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            return _db.Query<Category>("SELECT Id, Name, Description FROM dbo.Categories").ToList();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
