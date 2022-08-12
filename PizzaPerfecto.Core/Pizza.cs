using System;

namespace PizzaPerfecto.Core
{
    public class Pizza
    {
        public PizzaRecipe PizzaRecipe { get; }

        public Pizza(PizzaRecipe PizzaRecipe)
        {
            this.PizzaRecipe = PizzaRecipe;
        }

        public override string ToString(){
            return String.Format("State: Cooked, PizzaRecipe: {0}", PizzaRecipe);
        }
    }
}
