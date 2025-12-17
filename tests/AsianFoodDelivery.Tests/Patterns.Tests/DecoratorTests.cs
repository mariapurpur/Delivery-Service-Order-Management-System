using AsianFoodDelivery.Core.Decorators.ComboDecorators;
using AsianFoodDelivery.Core.Decorators.DishDecorators;
using AsianFoodDelivery.Core.Domain.Menu.Beverages;
using AsianFoodDelivery.Core.Domain.Menu.Combos;
using AsianFoodDelivery.Core.Domain.Menu.Dishes;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using Xunit;

namespace AsianFoodDelivery.Tests.Patterns.Tests;

public class DecoratorTests
{
    [Fact]
    public void DoublePortionDecorator_DoublePriceModifyName()
    {
        var baseItem = new Sushi("нигири", 2, new Money(100));
        var decorator = new DoublePortionDecorator(baseItem);

        var decoratedPrice = decorator.CalculatePrice();

        Assert.Equal(200m, decoratedPrice.Amount);
        Assert.Contains("двойная порция", decorator.Name);
    }

    [Fact]
    public void SpicyLevelDecorator_AddCostModifyName()
    {
        var baseItem = new Ramen("мисо", "тофу", new Money(250));
        var extraCost = new Money(30);
        var decorator = new SpicyLevelDecorator(baseItem, 5, extraCost);

        var decoratedPrice = decorator.CalculatePrice();

        Assert.Equal(280m, decoratedPrice.Amount);
        Assert.Contains("(уровень остроты: 5)", decorator.Name);
    }

    [Fact]
    public void SpicyLevelDecorator_SpicyLevelInvalid()
    {
        var baseItem = new Ramen("мисо", "тофу", new Money(250));
        var extraCost = new Money(30);

        Assert.Throws<ArgumentOutOfRangeException>(() => new SpicyLevelDecorator(baseItem, 0, extraCost));
        Assert.Throws<ArgumentOutOfRangeException>(() => new SpicyLevelDecorator(baseItem, 11, extraCost));
    }

    [Fact]
    public void PremiumBeverageDecorator_AddBeveragePrice()
    {
        var baseCombo = new BusinessLunch(new Money(350));
        var premiumBeverage = new Tea("зелёный", new Money(80));
        var decorator = new PremiumBeverageDecorator(baseCombo, premiumBeverage);

        var decoratedPrice = decorator.CalculatePrice();

        Assert.Equal(430m, decoratedPrice.Amount);
        Assert.Contains("с дополнительным напитком", decorator.Name);
    }

    [Fact]
    public void Decorator_AffectCalculatedPrice()
    {
        var baseItem = new Sushi("нигири", 2, new Money(100));
        var decorator = new DoublePortionDecorator(baseItem);

        var initialPrice = decorator.CalculatePrice();
        baseItem.Price = new Money(150);
        var priceAfterChange = decorator.CalculatePrice();

        Assert.Equal(200m, initialPrice.Amount);
        Assert.Equal(300m, priceAfterChange.Amount);
    }

    [Fact]
    public void Decorator_ModifyItem()
    {
        var baseItem = new Sushi("запечёные", 2, new Money(100));
        var decorator = new DoublePortionDecorator(baseItem);
        var newName = "изменённые суши";
        var newPrice = new Money(120);
        var newAvailability = false;

        decorator.Name = newName;
        decorator.Price = newPrice;
        decorator.IsAvailable = newAvailability;

        Assert.Equal(newName, baseItem.Name);
        Assert.Equal(newPrice, baseItem.Price);
        Assert.Equal(newAvailability, baseItem.IsAvailable);
        Assert.Contains("двойная порция", decorator.Name);
    }
}