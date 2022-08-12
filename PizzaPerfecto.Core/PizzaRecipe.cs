using System;

namespace PizzaPerfecto.Core
{
    public class PizzaRecipe
    {
        public String BaseType { get; }

        public String Topping { get; }

        public PizzaRecipe(String BaseType, String Topping) => (this.BaseType, this.Topping) = (BaseType, Topping);

        public override string ToString()
        {
            return String.Format("base: {0}, Toppings: {1}", BaseType, Topping);
        }
    }
}
