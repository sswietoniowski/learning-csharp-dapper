using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace DataAccess
{
    public class ContribCategoryRepository : ICategoryRepository
    {
        private IDbConnection _db;

        public ContribCategoryRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public Category Add(Category category)
        {
            var id = _db.Insert(category); 
            category.Id = (int)id;
            return category;
        }

        public Category Get(int id)
        {
            return _db.Get<Category>(id);
        }

        public List<Category> GetAll()
        {
            return _db.GetAll<Category>().ToList();
        }

        public Category GetWithProducts(int id)
        {
            throw new NotImplementedException();
        }

        public Category Modify(Category category)
        {
            _db.Update(category);
            return category;
        }

        public void Remove(int id)
        {
            _db.Delete<Category>(new Category { Id = id });
        }
    }
}
