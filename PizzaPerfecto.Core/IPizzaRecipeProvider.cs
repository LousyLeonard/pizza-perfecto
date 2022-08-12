using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPerfecto.Core
{
    public interface IPizzaRecipeProvider
    {
        public PizzaRecipe GetNextPizzaRecipe();
    }
}
