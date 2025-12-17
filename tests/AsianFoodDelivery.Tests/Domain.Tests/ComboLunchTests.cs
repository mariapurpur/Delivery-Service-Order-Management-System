using AsianFoodDelivery.Core.Domain.Menu.Combos;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using Xunit;

namespace AsianFoodDelivery.Tests.Domain.Tests;

public class ComboLunchTests
{
    [Fact]
    public void BusinessLunch_ConstructorNameAndPrice()
    {
        var basePrice = new Money(400);

        var combo = new BusinessLunch(basePrice);

        Assert.Equal("бизнес-ланч", combo.Name);
        Assert.Equal(basePrice, combo.Price);
        Assert.True(combo.IsAvailable);
    }

    [Fact]
    public void BusinessLunch_CalculatePrice_20PercentDiscount()
    {
        var basePrice = new Money(500);
        var combo = new BusinessLunch(basePrice);

        var item1 = new MenuItemStub(new Money(100));
        var item2 = new MenuItemStub(new Money(200));
        combo.AddItem(item1);
        combo.AddItem(item2);

        var totalPrice = combo.CalculatePrice();

        Assert.Equal(240m, totalPrice.Amount);
    }

    [Fact]
    public void BusinessLunch_CalculatePrice_NoItems()
    {
        var basePrice = new Money(400);
        var combo = new BusinessLunch(basePrice);

        var totalPrice = combo.CalculatePrice();

        Assert.Equal(320m, totalPrice.Amount);
    }

    private class MenuItemStub : IMenuItem
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = "тестовая позиция";
        public Money Price { get; set; }
        public bool IsAvailable { get; set; } = true;

        public MenuItemStub(Money price)
        {
            Price = price;
        }

        public Money CalculatePrice()
        {
            return Price;
        }
    }
}