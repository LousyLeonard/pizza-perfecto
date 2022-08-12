using Microsoft.Extensions.Configuration;
using PizzaPerfecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPerfecto.Services
{
    class RandomPizzaFactory : IPizzaRecipeProvider
    {
        private IEnumerable<String> Toppings;

        private IEnumerable<String> Base;

        public RandomPizzaFactory(IConfiguration config)
        {
            this.Toppings = config["Toppings"].Split(";");
            this.Base = config.GetSection("Bases")
                .GetChildren()
                .ToList()
                .Select(x => x.Key.ToString());
        }

        public PizzaRecipe GetNextPizzaRecipe()
        {
            return new PizzaRecipe(Base.RandomElement(), Toppings.RandomElement());
        }
    }
}
