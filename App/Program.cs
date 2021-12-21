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
            Category_Add_ShouldReturnProperId();
        }

        private static void Category_GetAll_ShouldReturnSomeObjects()
        {
            var repository = CreateRepository();
            var categories = repository.GetAll();
            Console.WriteLine($"Count: {categories.Count}");
            categories.Output();
        }

        private static void Category_Add_ShouldReturnProperId()
        {
            var repository = CreateRepository();
            var category = new Category { Name = "Komputery", Description = "Ciężko teraz żyć bez nich" };

            if (repository.GetAll().Where(c => c.Name == category.Name).Any())
            {
                Console.WriteLine("Kategoria o takiej nazwie już istnieje...");
                return;
            }

            repository.Add(category);
            if (!category.IsNew)
            {
                category.Output();
            }
            else
            {
                Console.WriteLine("Coś poszło nie tak, nie udało się dodać kategorii!");
            }
        }

        private static ICategoryRepository CreateRepository()
        {
            return new CategoryRepository(config.GetConnectionString("DefaultConnection"));
        }
    }
}