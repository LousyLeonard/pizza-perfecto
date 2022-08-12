
using Xunit;
using PizzaPerfecto.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq;
using PizzaPerfecto.Core;
using System.Collections.Generic;

namespace PizzaPerfecto.Test
{
    public class CookingTimeCalculatorShould
    {
        [Fact]
        public void CalculateTheRightToppingTimes_WithoutBase()
        {
            var config = new Mock<IConfiguration>();
            config.Setup(_ => _["Topping:CookTime"]).Returns("100");
            config.Setup(_ => _["Oven:BaseCookingTime"]).Returns("0");
            config.Setup(_ => _.GetSection("Bases").GetChildren()).Returns(Enumerable.Empty<IConfigurationSection>());

            var cookingTimeCalculator = new CookingTimeCalculator(config.Object);

            var pizzaRecipe = new PizzaRecipe("", "test");

            var result = cookingTimeCalculator.GetCookingTime(pizzaRecipe);

            Assert.Equal(400, result);
        }

        [Fact]
        public void CalculateTheBaseTime_WithoutMultiplerWithoutToppings()
        {
            var baseSection = new Mock<IConfigurationSection>();
            baseSection.Setup(s => s.Key).Returns("TestPan");
            baseSection.Setup(s => s.Value).Returns("1");

            var config = new Mock<IConfiguration>();
            config.Setup(_ => _["Topping:CookTime"]).Returns("100");
            config.Setup(_ => _["Oven:BaseCookingTime"]).Returns("100");
            config.Setup(_ => _.GetSection("Bases").GetChildren()).Returns(new List<IConfigurationSection>() { baseSection.Object });

            var cookingTimeCalculator = new CookingTimeCalculator(config.Object);

            var pizzaRecipe = new PizzaRecipe("TestPan", "");

            var result = cookingTimeCalculator.GetCookingTime(pizzaRecipe);

            Assert.Equal(100, result);
        }

        // TODO rework into a theory, this is basically two tests.
        [Fact]
        public void CalculateTheBaseTime_WithMultiplerWithoutToppings()
        {
            var testPanSection = new Mock<IConfigurationSection>();
            testPanSection.Setup(s => s.Key).Returns("TestPan");
            testPanSection.Setup(s => s.Value).Returns("2");

            var tenTestPanSection = new Mock<IConfigurationSection>();
            tenTestPanSection.Setup(s => s.Key).Returns("TenTestPan");
            tenTestPanSection.Setup(s => s.Value).Returns("10");

            var config = new Mock<IConfiguration>();
            config.Setup(_ => _["Topping:CookTime"]).Returns("100");
            config.Setup(_ => _["Oven:BaseCookingTime"]).Returns("100");
            config.Setup(_ => _.GetSection("Bases").GetChildren()).Returns(new List<IConfigurationSection>() { testPanSection.Object, tenTestPanSection.Object });

            var cookingTimeCalculator = new CookingTimeCalculator(config.Object);

            var pizzaRecipeDouble = new PizzaRecipe("TestPan", "");
            var pizzaRecipeTened = new PizzaRecipe("TenTestPan", "");

            var resultTwoTimesPan = cookingTimeCalculator.GetCookingTime(pizzaRecipeDouble);
            var resultTenTimesPan = cookingTimeCalculator.GetCookingTime(pizzaRecipeTened);

            Assert.Equal(200, resultTwoTimesPan);
            Assert.Equal(1000, resultTenTimesPan);
        }

        [Fact]
        public void CalculateWholeCookingTime_WithMultiplerWithToppings()
        {
            var testPanSection = new Mock<IConfigurationSection>();
            testPanSection.Setup(s => s.Key).Returns("TestPan");
            testPanSection.Setup(s => s.Value).Returns("2");

            var config = new Mock<IConfiguration>();
            config.Setup(_ => _["Topping:CookTime"]).Returns("100");
            config.Setup(_ => _["Oven:BaseCookingTime"]).Returns("1000");
            config.Setup(_ => _.GetSection("Bases").GetChildren()).Returns(new List<IConfigurationSection>() { testPanSection.Object });

            var cookingTimeCalculator = new CookingTimeCalculator(config.Object);

            var pizzaRecipe = new PizzaRecipe("TestPan", "Fakeroni");

            // 1000 * 2 + 100 * 8
            var result = cookingTimeCalculator.GetCookingTime(pizzaRecipe);

            Assert.Equal(2800, result);
        }
    }
}
