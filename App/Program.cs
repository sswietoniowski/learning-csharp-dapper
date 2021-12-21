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
            Category_Get_ShouldReturnProperObject();
            Category_Modify_ShouldChangeObject();
            Category_Remove_ShouldRemoveObject();
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

        private static void Category_Get_ShouldReturnProperObject()
        {
            var repository = CreateRepository();
            int id = repository.GetAll().Max(c => c.Id);
            var category = repository.Get(id);
            if (category.Id == id)
            {
                Console.WriteLine("Pobrana kategoria: ");
                category.Output();
            }
            else
            {
                Console.WriteLine("Nie udało się pobrać wybranej kategorii!");
            }
        }

        private static void Category_Modify_ShouldChangeObject()
        {
            var repository = CreateRepository();
            var id = 1;
            var category = repository.Get(id);
            Console.WriteLine("Kategoria przed zmianą: ");
            category.Description = "ZUPELNIE NOWY OPIS :-)";
            repository.Modify(category);
            var categoryAfterChange = repository.Get(id);
            if (categoryAfterChange.Description == category.Description)
            {
                Console.WriteLine("Zmiana została wprowadzona...");
                Console.WriteLine("Kategoria po zmianie: ");
                categoryAfterChange.Output();
            }
            else
            {
                Console.WriteLine("Nie udało się wykonać zmiany");
            }
        }

        private static void Category_Remove_ShouldRemoveObject()
        {
            var repository = CreateRepository();
            var id = repository.GetAll().Max(c => c.Id);
            repository.Remove(id);
            if (repository.Get(id) == null)
            {
                Console.WriteLine("Kategoria została usunięta");
            }
            else
            {
                Console.WriteLine("Nie udało się usunąć kategorii");
            }
        }

        private static ICategoryRepository CreateRepository()
        {
            return new CategoryRepository(config.GetConnectionString("DefaultConnection"));
        }
    }
}