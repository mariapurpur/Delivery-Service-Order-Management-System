using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Orders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsianFoodDelivery.Core.Domain.Entities;

public class Order : IOrder
{
    public Guid Id { get; }
    public User Customer { get; }
    public Address DeliveryAddress { get; set; }
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<IOrderItem> Items => _items.AsReadOnly().Cast<IOrderItem>().ToList();
    public Orders.OrderStatus Status { get; private set; }
    public Orders.OrderType Type { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime? DeliveredAt { get; private set; }

    public Order(User customer, Address deliveryAddress, Orders.OrderType type)
    {
        Id = Guid.NewGuid();
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress));
        Type = type;
        Status = Orders.OrderStatus.New;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(IOrderItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (item is OrderItem concreteItem)
        {
            _items.Add(concreteItem);
        }
        else
        {
            _items.Add((OrderItem)item);
        }
    }

    public bool RemoveItem(IOrderItem item)
    {
        if (item == null) return false;
        return _items.Remove((OrderItem)item);
    }

    public ValueObjects.Money CalculateTotalCost()
    {
        var total = new ValueObjects.Money(0);
        foreach (var item in _items)
        {
            total = total + item.TotalPrice;
        }
        return total;
    }

    public void UpdateStatus(Orders.OrderStatus newStatus)
    {
        Status = newStatus;
        if (newStatus == Orders.OrderStatus.Delivered)
        {
            DeliveredAt = DateTime.UtcNow;
        }
    }

    public int GetItemCount()
    {
        return _items.Count;
    }

    public bool IsEmpty()
    {
        return _items.Count == 0;
    }
}