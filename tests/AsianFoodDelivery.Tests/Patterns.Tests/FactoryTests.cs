using System.ComponentModel;
using System.Runtime.CompilerServices;
using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Domain.Menu.Beverages;
using AsianFoodDelivery.Core.Domain.Menu.Combos;
using AsianFoodDelivery.Core.Domain.Menu.Dishes;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Factories;
using AsianFoodDelivery.Core.Factories.Interfaces;
using AsianFoodDelivery.Core.Orders;
using Xunit;

namespace AsianFoodDelivery.Tests.Patterns.Tests;

public class FactoryTests
{
    private readonly User _testUser;
    private readonly Address _testAddress;
    private readonly IOrderFactory _orderFactory;
    private readonly IMenuItemFactory _menuItemFactory;

    public FactoryTests()
    {
        _testAddress = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        _testUser = new User("Василия Пупкина", _testAddress);
        _orderFactory = new OrderFactory();
        _menuItemFactory = new MenuItemFactory();
    }

    [Fact]
    public void OrderFactory_ReturnOrder()
    {
        var orderType = OrderType.Express;

        var order = _orderFactory.CreateOrder(_testUser, _testAddress, orderType);

        Assert.NotNull(order);
        Assert.Equal(orderType, order.Type);
        Assert.Same(_testUser, order.Customer);
        Assert.Same(_testAddress, order.DeliveryAddress);
        Assert.Equal(OrderStatus.New, order.Status);
    }

    [Fact]
    public void MenuItemFactory_BubbleTea()
    {
        var name = "бабл ти";
        var price = new Money(250);
        var flavor = "манго";
        var hasBoba = true;
        var description = "";

        var item = _menuItemFactory.CreateMenuItem(MenuItemType.BubbleTea, flavor, hasBoba, price, description);

        Assert.NotNull(item);
        Assert.IsType<BubbleTea>(item);
        Assert.Contains(flavor, item.Name);
        Assert.Equal(name, item.Name);
        Assert.Equal(price, item.Price);
        Assert.Equal(flavor, ((BubbleTea)item).Flavor);
        Assert.Equal(hasBoba, ((BubbleTea)item).HasBoba);
        Assert.True(item.IsAvailable);
    }

    [Fact]
    public void MenuItemFactory_Tea()
    {
        var name = "чаёк";
        var price = new Money(100);
        var type = "чёрный";
        var description = "";

        var item = _menuItemFactory.CreateMenuItem(MenuItemType.Tea, type, price, description);

        Assert.NotNull(item);
        Assert.IsType<Tea>(item);
        Assert.Contains(type, item.Name);
        Assert.Equal(name, item.Name);
        Assert.Equal(price, item.Price);
        Assert.Equal(type, ((Tea)item).Type);
        Assert.True(item.IsAvailable);
    }

    [Fact]
    public void MenuItemFactory_Ramen()
    {
        var name = "рамён";
        var price = new Money(300);
        var brothType = "куриный";
        var protein = "курица";
        var description = "";

        var item = _menuItemFactory.CreateMenuItem(MenuItemType.Ramen, brothType, protein, price, description);

        Assert.NotNull(item);
        Assert.IsType<Ramen>(item);
        Assert.Contains(brothType, item.Name);
        Assert.Contains(protein, item.Name);
        Assert.Equal(name, item.Name);
        Assert.Equal(price, item.Price);
        Assert.Equal(brothType, ((Ramen)item).BrothType);
        Assert.Equal(protein, ((Ramen)item).Protein);
        Assert.True(item.IsAvailable);
    }

    [Fact]
    public void MenuItemFactory_Sushi()
    {
        var name = "суши";
        var price = new Money(250);
        var type = "запечёные";
        var pieces = 8;
        var description = "";

        var item = _menuItemFactory.CreateMenuItem(MenuItemType.Sushi, type, pieces, price, description);

        Assert.NotNull(item);
        Assert.IsType<Sushi>(item);
        Assert.Contains(type, item.Name);
        Assert.Contains(pieces.ToString(), item.Name);
        Assert.Equal(name, item.Name);
        Assert.Equal(price, item.Price);
        Assert.Equal(type, ((Sushi)item).Type);
        Assert.Equal(pieces, ((Sushi)item).Pieces);
        Assert.True(item.IsAvailable);
    }

    [Fact]
    public void MenuItemFactory_BusinessLunch()
    {
        var price = new Money(400);

        var item = _menuItemFactory.CreateMenuItem(MenuItemType.BusinessLunch, price);

        Assert.NotNull(item);
        Assert.IsType<BusinessLunch>(item);
        Assert.Equal("бизнес-ланч", item.Name);
        Assert.Equal(price, item.Price);
        Assert.True(item.IsAvailable);
    }

    [Fact]
    public void MenuItemFactory_ArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => _menuItemFactory.CreateMenuItem((MenuItemType)999));
        Assert.Contains("такого у нас в меню нет...", exception.Message);
    }
}