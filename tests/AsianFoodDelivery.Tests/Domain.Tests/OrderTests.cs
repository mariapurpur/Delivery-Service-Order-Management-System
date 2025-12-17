using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using System;
using Xunit;

namespace AsianFoodDelivery.Tests.Domain.Tests;

public class OrderTests
{
    private readonly User _testUser;
    private readonly Address _testAddress;
    private readonly Order _order;

    public OrderTests()
    {
        _testAddress = new Address("тестовая ул.", "такой-то город", "такой-то регион", "10", "5");
        _testUser = new User("Вася Пупкин", _testAddress);
        _order = new Order(_testUser, _testAddress, OrderType.Regular);
    }

    [Fact]
    public void Constructor_InitializeProperties()
    {
        Assert.NotEqual(Guid.Empty, _order.Id);
        Assert.Same(_testUser, _order.Customer);
        Assert.Same(_testAddress, _order.DeliveryAddress);
        Assert.Equal(OrderType.Regular, _order.Type);
        Assert.Equal(OrderStatus.New, _order.Status);
        Assert.Null(_order.DeliveredAt);
        Assert.Empty(_order.Items);
    }

    [Fact]
    public void AddItem_AddItem()
    {
        var item = new OrderItem(new Money(100), 2);

        _order.AddItem(item);

        Assert.Single(_order.Items);
        Assert.Contains(item, _order.Items);
    }

    [Fact]
    public void AddItem_NullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _order.AddItem(null!));
        Assert.Equal("позиция", exception.ParamName);
    }

    [Fact]
    public void RemoveItem_RemoveItemFromList()
    {
        var item = new OrderItem(new Money(100), 2);
        _order.AddItem(item);

        var result = _order.RemoveItem(item);

        Assert.True(result);
        Assert.Empty(_order.Items);
    }

    [Fact]
    public void RemoveItem_ItemIsNotFound()
    {
        var item1 = new OrderItem(new Money(100), 2);
        var item2 = new OrderItem(new Money(50), 1);
        _order.AddItem(item1);

        var result = _order.RemoveItem(item2);

        Assert.False(result);
        Assert.Single(_order.Items);
    }

    [Fact]
    public void CalculateTotalCost_NoItems()
    {
        var total = _order.CalculateTotalCost();

        Assert.Equal(0m, total.Amount);
    }

    [Fact]
    public void CalculateTotalCost_MultipleItems()
    {
        var item1 = new OrderItem(new Money(100), 2);
        var item2 = new OrderItem(new Money(50), 3);
        _order.AddItem(item1);
        _order.AddItem(item2);

        var total = _order.CalculateTotalCost();

        Assert.Equal(350m, total.Amount);
    }

    [Fact]
    public void UpdateStatus_StatusDelivered()
    {
        _order.UpdateStatus(OrderStatus.Delivered);

        Assert.Equal(OrderStatus.Delivered, _order.Status);
        Assert.NotNull(_order.DeliveredAt);
    }

    [Fact]
    public void UpdateStatus_StatusIsNotDelivered()
    {
        _order.UpdateStatus(OrderStatus.Preparing);

        Assert.Equal(OrderStatus.Preparing, _order.Status);
        Assert.Null(_order.DeliveredAt);
    }

    [Fact]
    public void GetItemCount_ReturnCount()
    {
        var item1 = new OrderItem(new Money(100), 2);
        var item2 = new OrderItem(new Money(50), 1);
        _order.AddItem(item1);
        _order.AddItem(item2);

        var count = _order.GetItemCount();

        Assert.Equal(2, count);
    }

    [Fact]
    public void IsEmpty_NoItems()
    {
        var isEmpty = _order.IsEmpty();

        Assert.True(isEmpty);
    }

    [Fact]
    public void IsEmpty_HasItems()
    {
        var item = new OrderItem(new Money(100), 2);
        _order.AddItem(item);

        var isEmpty = _order.IsEmpty();

        Assert.False(isEmpty);
    }
}