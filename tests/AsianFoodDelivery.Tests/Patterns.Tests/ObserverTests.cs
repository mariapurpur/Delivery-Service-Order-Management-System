using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Observers.Interfaces;
using AsianFoodDelivery.Core.Observers.Observers;
using Moq;
using Xunit;

namespace AsianFoodDelivery.Tests.Patterns.Tests;

public class ObserverTests
{
    private readonly User _testUser;
    private readonly Address _testAddress;
    private readonly Order _order;

    public ObserverTests()
    {
        _testAddress = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        _testUser = new User("Василий Пупкин", _testAddress);
        _order = new Order(_testUser, _testAddress, OrderType.Regular);
    }

    [Fact]
    public void Order_ObserversOperations()
    {
        var mockObserver = new Mock<IOrderObserver>();
        var mockObserver2 = new Mock<IOrderObserver>();

        _order.Attach(mockObserver.Object);
        _order.Attach(mockObserver2.Object);
        _order.Detach(mockObserver.Object);

        _order.UpdateStatus(OrderStatus.Confirmed);
        mockObserver.Verify(o => o.Update(It.IsAny<Order>()), Times.Never());
        mockObserver2.Verify(o => o.Update(It.IsAny<Order>()), Times.Once());
    }

    [Fact]
    public void Order_NotifyOnStatusChange()
    {
        var userNotifier = new UserNotifier();
        var restaurantNotifier = new RestaurantNotifier();
        var courierNotifier = new CourierNotifier();

        _order.Attach(userNotifier);
        _order.Attach(restaurantNotifier);
        _order.Attach(courierNotifier);

        _order.UpdateStatus(OrderStatus.Confirmed);

        var counterObserver = new CounterObserver();
        _order.Attach(counterObserver);

        _order.UpdateStatus(OrderStatus.Delivering);

        Assert.Equal(1, counterObserver.UpdateCount);
    }

    [Fact]
    public void Order_AfterDetaching()
    {
        var userNotifier = new UserNotifier();
        _order.Attach(userNotifier);

        _order.Detach(userNotifier);
        _order.UpdateStatus(OrderStatus.Ready);
    }

    private class CounterObserver : IOrderObserver
    {
        public int UpdateCount { get; private set; } = 0;

        public void Update(Order order)
        {
            UpdateCount++;
        }
    }
}