using System;
using System.Threading.Tasks;

namespace PizzaPerfecto.Core
{
    public interface IOven
    {
        Action CookPizza(PizzaRecipe pizzaRecipe);
    }
}
