using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PizzaPerfecto.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaPerfecto.Services
{
    class PizzaFactory : IPizzaFactory
    {
        private readonly int PizzasToProduce;

        private readonly int CookingInterval;

        private readonly IOven Oven;

        private readonly IPizzaRecipeProvider recipeProvider;

        private readonly ILogger<PizzaFactory> Logger;

        public PizzaFactory(IConfiguration config, IOven Oven, IPizzaRecipeProvider recipeProvider, ILoggerFactory loggerFactory)
        {
            this.PizzasToProduce = int.Parse(config["Factory:NoToProduce"]);
            this.CookingInterval = int.Parse(config["Factory:TimeBetweenProduction"]);
            this.Oven = Oven;
            this.recipeProvider = recipeProvider;
            this.Logger = loggerFactory.CreateLogger<PizzaFactory>();
        }

        public void CookRandomPizzas(int count)
        {
            var cookingPizzas = new List<Task>();
            for (int i = 0; i < PizzasToProduce; ++i)
            {
                Task cookingPizza = Task.Factory.StartNew(Oven.CookPizza(recipeProvider.GetNextPizzaRecipe()));
                cookingPizzas.Add(cookingPizza);

                Thread.Sleep(CookingInterval);
            }

            Task.WaitAll(cookingPizzas.ToArray());
            Logger.LogDebug("All Pizzas cooked. Exiting.");
            Environment.Exit(1);
        }
    }
}
