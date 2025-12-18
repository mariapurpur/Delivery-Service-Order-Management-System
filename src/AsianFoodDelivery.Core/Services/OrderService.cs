using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Orders.Interfaces;
using AsianFoodDelivery.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AsianFoodDelivery.Core.Services;

public class OrderService : IOrderService
{
    private readonly List<Order> _orders = new();

    public Order CreateOrder(User customer, Address deliveryAddress, OrderType type)
    {
        var order = new Order(customer, deliveryAddress, type);
        _orders.Add(order);
        return order;
    }

    public Order? GetOrderById(Guid id)
    {
        return _orders.FirstOrDefault(order => order.Id == id);
    }

    public IReadOnlyList<Order> GetAllOrders()
    {
        return _orders.AsReadOnly();
    }

    public void UpdateOrderStatus(Guid orderId, OrderStatus newStatus)
    {
        var order = GetOrderById(orderId);
        if (order == null) throw new ArgumentException("такого заказа нет...", nameof(orderId));

        order.UpdateStatus(newStatus);
    }

    public void AddItemToOrder(Guid orderId, IOrderItem item)
    {
        var order = GetOrderById(orderId);
        if (order == null) throw new ArgumentException("такого заказа нет...", nameof(orderId));

        order.AddItem(item);
    }

    public bool RemoveItemFromOrder(Guid orderId, IOrderItem item)
    {
        var order = GetOrderById(orderId);
        if (order == null) throw new ArgumentException(("такого заказа нет..."), nameof(orderId));

        return order.RemoveItem(item);
    }
}