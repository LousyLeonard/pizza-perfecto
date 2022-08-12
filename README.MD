# Pizza Factory
C# Console Solution to the Pizza Factory, see the [full spec](https://github.com/LousyLeonard/pizza-perfecto/blob/master/PizzaPerfectoSpec.pdf).

### Quick Start
Open PizzaPerfecto.sln in Visual Studio.
Run PizzaPerfecto.cs
Settings can be found in appsettings.json.
By default the output folder is C:/temp/PizzaLog.txt

### Settings
Can be found in appsettings.json.
*Toppings*: Semicolon separated string of toppings.
*Bases*: Json children of "\<Base\>" : Multipler.
*Topping:CookTime*: Cooking time multipler to apply to each letter of the topping in Millisecond.
*Factory:NoToProduce*: Number of pizzas to produce.
*Factory:TimeBetweenProduction*: Wait time between putting pizzas in the oven in Milliseconds.
*Oven:BaseCookingTime*: The base cooking time of a pizza in the oven in Milliseconds.
*OutputFile*: Full file path, name, and extension on the output file.

### Dependencies
Microsoft.Extensions.Configuration.Binder - Added to allow better configuration file support.
Microsoft.Extensions.Configuration.Json - Added to allow configuration binding to json files.
Microsoft.Extensions.DependencyInjection - Added to activate the dependency inject framework.
Microsoft.Extensions.Logging - Added to allow logging capabilities.
Microsoft.Extensions.Logging.Console - Added to allow logging to the console.
NReco.Logging.File - Added to allow logging to a file.

### Discussion
Further considerations:

I don't get my threads from a pool meaning we might be creating and destroying extra threads.

I've decided to call upon the dependency injection framework even though this is a console application. This allows me to write good easily testable classes and actually just makes everything a bit simplier to write.

"Therefore, given a Deep Pan Pepperoni pizza is ordered and the base cooking time is 3000ms then  the total cooking time will be 6900ms."
There's some ambiguity in whether the base multipler should apply to Toppings. The above calculation doesn't apply it this way. So I've decided to only apply it to the base cooking time.
-    (Base Cooking Time x Base Multipler) + (Length of Topping names x Configured Topping Multipler)

I've initially tried to split out the common classes from their implementation in terms of a core class library. This will eventually benefit us if we move to producing a second application/service in the area, useful for micro services.

Testing is only partially implemented, I just had to put this down at some point and that means not everything gets finished. I prioritised the code over testing because its just easier to see the application working that trying to assess a half finished project.

The export to file capability is actually just redirecting the logging to a file. It fits the bill for the spec provided.

For the CookingTimeCalculatorShould tests, I probably would've just create some test json files and loaded them in the same way I initially created the config in PizzaPerfecto.cs. Instead I've mocked them purely to demostrate mocking.
