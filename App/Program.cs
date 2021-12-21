using DataAccess;
using Domain;
using Microsoft.Extensions.Configuration;

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
        // TODO: dodać przykłady wywołania Dapper-a
    }

    private static ICategoryRepository CreateRepository()
    {
        return new CategoryRepository(config.GetConnectionString("DefaultConnection"));
    }
}
