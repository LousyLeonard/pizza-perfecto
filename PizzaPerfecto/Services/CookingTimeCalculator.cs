using Microsoft.Extensions.Configuration;
using PizzaPerfecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPerfecto.Services
{
    public class CookingTimeCalculator : ICookingTimeCalculator
    {
        private readonly int ToppingCookTime;

        private readonly IEnumerable<IConfigurationSection> BaseMultiplers;

        private readonly int BaseCookingTime;

        public CookingTimeCalculator(IConfiguration config)
        {
            this.ToppingCookTime = int.Parse(config["Topping:CookTime"]);
            this.BaseMultiplers = config.GetSection("Bases")
                        .GetChildren()
                        .ToList()
                        .Select(x => x);
            this.BaseCookingTime = int.Parse(config["Oven:BaseCookingTime"]);
        }

        public int GetCookingTime(PizzaRecipe pizza)
        {
            return Convert.ToInt32((this.BaseCookingTime * GetMultiplerForBase(pizza)) + (pizza.Topping.Length * this.ToppingCookTime));
        }

        private double GetMultiplerForBase(PizzaRecipe pizza)
        {
            return BaseMultiplers.Where(e => e.Key == pizza.BaseType)
                .Select(e => double.Parse(e.Value))
                .DefaultIfEmpty(1)
                .First();
        }
    }
}
