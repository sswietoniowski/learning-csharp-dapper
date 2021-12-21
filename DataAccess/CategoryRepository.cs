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
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            return _db.Query<Category>("SELECT Id, Name, Description AS Info FROM dbo.Categories").ToList();
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
