using App;
using DataAccess;
using Domain;
using Microsoft.Extensions.Configuration;

namespace App
{
    public class Program
    {
        private static IConfigurationRoot config;

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config = builder.Build();
        }

        public static void Main(string[] args)
        {
            Initialize();
            Category_GetAll_ShouldReturnSomeObjects();
        }

        private static void Category_GetAll_ShouldReturnSomeObjects()
        {
            var repository = CreateRepository();
            var categories = repository.GetAll();
            Console.WriteLine($"Count: {categories.Count}");
            categories.Output();
        }

        private static ICategoryRepository CreateRepository()
        {
            return new CategoryRepository(config.GetConnectionString("DefaultConnection"));
        }
    }
}