using AsianFoodDelivery.Core.Domain.Menu.Beverages;
using AsianFoodDelivery.Core.Domain.Menu.Dishes;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using Xunit;

namespace AsianFoodDelivery.Tests.Domain.Tests;

public class MenuItemTests
{
    [Fact]
    public void MenuItemBase_ConstructorProperties()
    {
        var name = "тестовая позиция";
        var price = new Money(100);
        var isAvailable = true;

        var menuItem = new MenuItemStub(name, price, isAvailable);

        Assert.NotEqual(Guid.Empty, menuItem.Id);
        Assert.Equal(name, menuItem.Name);
        Assert.Equal(price, menuItem.Price);
        Assert.Equal(isAvailable, menuItem.IsAvailable);
    }

    [Fact]
    public void MenuItemBase_CalculatePrice()
    {
        var price = new Money(150);
        var menuItem = new MenuItemStub("позиция", price);

        var calculatedPrice = menuItem.CalculatePrice();

        Assert.Equal(price, calculatedPrice);
    }

    [Fact]
    public void Sushi_ConstructorProperties()
    {
        var type = "крабовые";
        var pieces = 8;
        var price = new Money(200);

        var sushi = new Sushi(type, pieces, price);

        Assert.Equal($"{type} суши, 8 шт.", sushi.Name);
        Assert.Equal(price, sushi.Price);
        Assert.Equal(type, sushi.Type);
        Assert.Equal(pieces, sushi.Pieces);
        Assert.True(sushi.IsAvailable);
    }

    [Fact]
    public void Tea_ConstructorProperties()
    {
        var type = "зелёный";
        var price = new Money(80);

        var tea = new Tea(type, price);

        Assert.Equal($"чай ({type})", tea.Name);
        Assert.Equal(price, tea.Price);
        Assert.Equal(type, tea.Type);
        Assert.True(tea.IsAvailable);
    }

    private class MenuItemStub : MenuItemBase
    {
        public MenuItemStub(string name, Money price, bool isAvailable = true)
            : base(name, price, isAvailable)
        {
        }
    }
}