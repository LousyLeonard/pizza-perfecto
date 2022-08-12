using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPerfecto.Core
{
    public interface ICookingTimeCalculator
    {
        int GetCookingTime(PizzaRecipe pizzaRecipe);
    }
}
