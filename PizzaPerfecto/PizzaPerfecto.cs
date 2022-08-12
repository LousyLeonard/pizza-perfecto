using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PizzaPerfecto.Core;
using PizzaPerfecto.Services;
using Microsoft.Extensions.Configuration;

namespace PizzaPerfecto
{
    class PizzaPerfecto
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var loggingFile = configuration["OutputFile"];

            var serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddFile(loggingFile, append: false);
                })
                .AddSingleton<IConfiguration> (configuration)
                .AddSingleton<ICookingTimeCalculator, CookingTimeCalculator>()
                .AddSingleton<IOven, Oven>()
                .AddSingleton<IPizzaFactory, PizzaFactory>()
                .AddSingleton<IPizzaRecipeProvider, RandomPizzaFactory>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<PizzaPerfecto>();

            logger.LogDebug("Starting application");

            //do the actual work here
            var pizzaFactory = serviceProvider.GetService<IPizzaFactory>();
            pizzaFactory.CookRandomPizzas(50);

            logger.LogDebug("All done!");
        }
    }
}
