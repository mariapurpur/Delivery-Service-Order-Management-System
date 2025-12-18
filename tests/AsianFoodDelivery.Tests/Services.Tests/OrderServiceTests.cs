using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Services;
using AsianFoodDelivery.Core.Services.Interfaces;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using Moq;
using Xunit;

namespace AsianFoodDelivery.Tests.Services.Tests;

public class OrderServiceTests
{
    private readonly OrderService _orderService;
    private readonly Mock<IPricingService> _mockPricingService;
    private readonly User _testUser;
    private readonly Address _testAddress;

    public OrderServiceTests()
    {
        _mockPricingService = new Mock<IPricingService>();
        _orderService = new OrderService();
        _testAddress = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        _testUser = new User("Василий Пупкин", _testAddress);
    }

    [Fact]
    public void CreateOrder_Test()
    {
        var orderType = OrderType.Regular;

        var order = _orderService.CreateOrder(_testUser, _testAddress, orderType);

        Assert.NotNull(order);
        Assert.Equal(orderType, order.Type);
        Assert.Same(_testUser, order.Customer);
        Assert.Same(_testAddress, order.DeliveryAddress);
        Assert.Equal(OrderStatus.New, order.Status);
        Assert.Single(_orderService.GetAllOrders());
        Assert.Contains(order, _orderService.GetAllOrders());
    }

    [Fact]
    public void GetOrderById_CorrectOrder()
    {
        var orderType = OrderType.Regular;
        var order = _orderService.CreateOrder(_testUser, _testAddress, orderType);
        var orderId = order.Id;

        var retrievedOrder = _orderService.GetOrderById(orderId);

        Assert.NotNull(retrievedOrder);
        Assert.Equal(orderId, retrievedOrder.Id);
        Assert.Same(order, retrievedOrder);
    }

    [Fact]
    public void GetOrderById_OrderDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();

        var retrievedOrder = _orderService.GetOrderById(nonExistentId);

        Assert.Null(retrievedOrder);
    }

    [Fact]
    public void GetAllOrders_Test()
    {
        var orderType = OrderType.Regular;
        var order1 = _orderService.CreateOrder(_testUser, _testAddress, orderType);
        var order2 = _orderService.CreateOrder(_testUser, _testAddress, OrderType.Express);

        var allOrders = _orderService.GetAllOrders();

        Assert.Equal(2, allOrders.Count);
        Assert.Contains(order1, allOrders);
        Assert.Contains(order2, allOrders);
    }

    [Fact]
    public void UpdateOrderStatus_Test()
    {
        var orderType = OrderType.Regular;
        var order = _orderService.CreateOrder(_testUser, _testAddress, orderType);
        var initialStatus = order.Status;
        var newStatus = OrderStatus.Confirmed;

        Assert.Equal(OrderStatus.New, initialStatus);

        _orderService.UpdateOrderStatus(order.Id, newStatus);

        Assert.Equal(newStatus, order.Status);
    }

    [Fact]
    public void UpdateOrderStatus_OrderDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var newStatus = OrderStatus.Confirmed;

        var exception = Assert.Throws<ArgumentException>(() => _orderService.UpdateOrderStatus(nonExistentId, newStatus));
        Assert.Contains("такого заказа нет...", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("orderId", exception.ParamName);
    }

    [Fact]
    public void AddItemToOrder_AddsItem()
    {
        var orderType = OrderType.Regular;
        var order = _orderService.CreateOrder(_testUser, _testAddress, orderType);
        var menuItemStub = new MenuItemStub(new Money(100));
        var orderItem = new OrderItem(menuItemStub, 2);

        _orderService.AddItemToOrder(order.Id, orderItem);

        Assert.Single(order.Items);
        Assert.Contains(orderItem, order.Items);
    }

    [Fact]
    public void AddItemToOrder_OrderDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);

        var exception = Assert.Throws<ArgumentException>(() => _orderService.AddItemToOrder(nonExistentId, item));
        Assert.Contains("такого заказа нет...", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("orderId", exception.ParamName);
    }

    [Fact]
    public void RemoveItem_Test()
    {
        var orderType = OrderType.Regular;
        var order = _orderService.CreateOrder(_testUser, _testAddress, orderType);
        var menuItemStub = new MenuItemStub(new Money(100));
        var orderItem = new OrderItem(menuItemStub, 2);
        order.AddItem(orderItem);

        var result = _orderService.RemoveItemFromOrder(order.Id, orderItem);

        Assert.True(result);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void RemoveItem_ItemNotInOrder()
    {
        var orderType = OrderType.Regular;
        var order = _orderService.CreateOrder(_testUser, _testAddress, orderType);
        var menuItemStub1 = new MenuItemStub(new Money(100));
        var menuItemStub2 = new MenuItemStub(new Money(50));
        var orderItem1 = new OrderItem(menuItemStub1, 2);
        var orderItem2 = new OrderItem(menuItemStub2, 1);
        order.AddItem(orderItem1);

        var result = _orderService.RemoveItemFromOrder(order.Id, orderItem2);

        Assert.False(result);
        Assert.Single(order.Items);
        Assert.Contains(orderItem1, order.Items);
    }

    [Fact]
    public void RemoveItem_OrderDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);

        var exception = Assert.Throws<ArgumentException>(() => _orderService.RemoveItemFromOrder(nonExistentId, item));
        Assert.Contains("такого заказа нет...", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("orderId", exception.ParamName);
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