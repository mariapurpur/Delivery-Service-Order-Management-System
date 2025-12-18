using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using Xunit;

namespace AsianFoodDelivery.Tests.Patterns.Tests;

public class StateTests
{
    private readonly User _testUser;
    private readonly Address _testAddress;
    private readonly Order _order;

    public StateTests()
    {
        _testAddress = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        _testUser = new User("Василий Пупкин", _testAddress);
        _order = new Order(_testUser, _testAddress, OrderType.Regular);
    }

    [Fact]
    public void Order_NewState()
    {
        Assert.Equal(OrderStatus.New, _order.Status);
    }

    [Fact]
    public void NewState_AddingItems()
    {
        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);

        _order.AddItem(item);

        Assert.Single(_order.Items);
        Assert.Contains(item, _order.Items);
    }

    [Fact]
    public void NewState_RemovingItems()
    {
        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
        _order.AddItem(item);

        var result = _order.RemoveItem(item);

        Assert.True(result);
        Assert.Empty(_order.Items);
    }

    [Fact]
    public void NewState_UpdatingConfirmed()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);

        Assert.Equal(OrderStatus.Confirmed, _order.Status);
    }

    [Fact]
    public void NewState_UpdatingCancelled()
    {
        _order.UpdateStatus(OrderStatus.Cancelled);

        Assert.Equal(OrderStatus.Cancelled, _order.Status);
    }

    [Fact]
    public void PreparingState_AddingItems()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
    }

    [Fact]
    public void PreparingState_RemovingItems()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
        _order.AddItem(item);
    }

    [Fact]
    public void PreparingState_UpdatingReady()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);

        _order.UpdateStatus(OrderStatus.Ready);

        Assert.Equal(OrderStatus.Ready, _order.Status);
    }

    [Fact]
    public void PreparingState_UpdatingCancelled()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);

        _order.UpdateStatus(OrderStatus.Cancelled);

        Assert.Equal(OrderStatus.Cancelled, _order.Status);
    }

    [Fact]
    public void DeliveringState_AddingItems()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);
        _order.UpdateStatus(OrderStatus.Ready);
        _order.UpdateStatus(OrderStatus.Delivering);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
    }

    [Fact]
    public void DeliveringState_RemovingItems()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);
        _order.UpdateStatus(OrderStatus.Ready);
        _order.UpdateStatus(OrderStatus.Delivering);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
        _order.AddItem(item);
    }

    [Fact]
    public void DeliveringState_UpdatingDelivered()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);
        _order.UpdateStatus(OrderStatus.Ready);
        _order.UpdateStatus(OrderStatus.Delivering);

        _order.UpdateStatus(OrderStatus.Delivered);

        Assert.Equal(OrderStatus.Delivered, _order.Status);
        Assert.NotNull(_order.DeliveredAt);
    }

    [Fact]
    public void CompletedState_AddingItems()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);
        _order.UpdateStatus(OrderStatus.Ready);
        _order.UpdateStatus(OrderStatus.Delivering);
        _order.UpdateStatus(OrderStatus.Delivered);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
    }

    [Fact]
    public void CompletedState_RemovingItems()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);
        _order.UpdateStatus(OrderStatus.Ready);
        _order.UpdateStatus(OrderStatus.Delivering);
        _order.UpdateStatus(OrderStatus.Delivered);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
        _order.AddItem(item);
    }

    [Fact]
    public void CompletedState_UpdatingStatus()
    {
        _order.UpdateStatus(OrderStatus.Confirmed);
        _order.UpdateStatus(OrderStatus.Preparing);
        _order.UpdateStatus(OrderStatus.Ready);
        _order.UpdateStatus(OrderStatus.Delivering);
        _order.UpdateStatus(OrderStatus.Delivered);
    }

    [Fact]
    public void CancelledState_AddingItems()
    {
        _order.UpdateStatus(OrderStatus.Cancelled);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
    }

    [Fact]
    public void CancelledState_RemovingItems()
    {
        _order.UpdateStatus(OrderStatus.Cancelled);

        var item = new OrderItem(new MenuItemStub(new Money(100)), 2);
        _order.AddItem(item);
    }

    [Fact]
    public void CancelledState_UpdatingStatus()
    {
        _order.UpdateStatus(OrderStatus.Cancelled);
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