using Microsoft.Extensions.Logging;
using PizzaPerfecto.Core;
using System;
using System.Threading;

namespace PizzaPerfecto.Services
{
    class Oven : IOven
    {
        private readonly ICookingTimeCalculator cookingTimeCalculator;
        private readonly ILogger<Oven> Logger;

        public Oven(ICookingTimeCalculator cookingTimeCalculator, ILoggerFactory loggerFactory)
        {
            this.cookingTimeCalculator = cookingTimeCalculator;
            this.Logger = loggerFactory.CreateLogger<Oven>();
        }

        public Action CookPizza(PizzaRecipe pizzaRecipe)
        {
            return () =>
            {
                var timeToSleep = this.cookingTimeCalculator.GetCookingTime(pizzaRecipe);

                this.Logger.LogDebug("Putting pizza in the oven for: {0}ms", timeToSleep);
                Thread.Sleep(timeToSleep);
                var pizza = new Pizza(pizzaRecipe);
                this.Logger.LogInformation("Finished pizza: {0}", pizza);
            };
        }

    }
}
