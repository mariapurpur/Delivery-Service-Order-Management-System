using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Services;
using AsianFoodDelivery.Core.Domain.Menu.Combos;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Utilities;
using Moq;
using Xunit;

namespace AsianFoodDelivery.Tests.Services.Tests;

public class PricingServiceTests
{
    private readonly Mock<ITimeService> _mockTimeService;
    private readonly PricingService _pricingService;

    public PricingServiceTests()
    {
        _mockTimeService = new Mock<ITimeService>();
        _pricingService = new PricingService(_mockTimeService.Object);
    }

    [Fact]
    public void CalculateTotalPrice_NoItems()
    {
        var order = new Order(new User("Василий Пупкин", new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5")), new Address("Альпийский переулок", "Санкт-Петербург", "Ленинградская область", "10", "5"), OrderType.Regular);

        var totalPrice = _pricingService.CalculateTotalPrice(order);

        Assert.Equal(0m, totalPrice.Amount);
    }

    [Fact]
    public void CalculateTotalPrice_CorrectTotal()
    {
        var order = new Order(new User("Василий Пупкин", new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5")), new Address("Альпийский переулок", "Санкт-Петербург", "Ленинградская область", "10", "5"), OrderType.Regular);
        var item1 = new OrderItem(new MenuItemStub(new Money(100)), 2);
        var item2 = new OrderItem(new MenuItemStub(new Money(50)), 3);
        order.AddItem(item1);
        order.AddItem(item2);

        var totalPrice = _pricingService.CalculateTotalPrice(order);

        Assert.Equal(350m, totalPrice.Amount);
    }

    [Fact]
    public void CalculateTotalPrice_ComboDiscount()
    {
        var order = new Order(new User("Василий Пупкин", new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5")), new Address("Альпийский переулок", "Санкт-Петербург", "Ленинградская область", "10", "5"), OrderType.Regular);
        var combo = new BusinessLunch(new Money(350), "");
        var item1 = new MenuItemStub(new Money(100));
        var item2 = new MenuItemStub(new Money(150));
        combo.AddItem(item1);
        combo.AddItem(item2);
        var orderItem = new OrderItem(combo, 1);
        order.AddItem(orderItem);

        _mockTimeService.Setup(ts => ts.GetCurrentTime()).Returns(new DateTime(2023, 1, 1, 13, 0, 0)); // 13:00

        var totalPrice = _pricingService.CalculateTotalPrice(order);

        Assert.Equal(350m, totalPrice.Amount);
    }

    [Fact]
    public void CalculateTotalPrice_TimeDiscount()
    {
        var order = new Order(new User("Василий Пупкин", new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5")), new Address("Альпийский переулок", "Санкт-Петербург", "Ленинградская область", "10", "5"), OrderType.Regular);
        var item1 = new OrderItem(new MenuItemStub(new Money(100)), 2);
        var item2 = new OrderItem(new MenuItemStub(new Money(50)), 3);
        order.AddItem(item1);
        order.AddItem(item2);

        _mockTimeService.Setup(ts => ts.GetCurrentTime()).Returns(new DateTime(2023, 1, 1, 22, 0, 0)); // 22:00

        var totalPrice = _pricingService.CalculateTotalPrice(order);

        Assert.Equal(315m, totalPrice.Amount);
    }

    private class MenuItemStub : IMenuItem
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = "тестовая позиция";
        public string Description { get; set; } = "";
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