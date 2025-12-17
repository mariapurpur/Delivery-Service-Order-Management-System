using AsianFoodDelivery.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AsianFoodDelivery.Core.Orders.Interfaces;

public interface IOrder
{
    Guid Id { get; }
    User Customer { get; }
    Address DeliveryAddress { get; set; }
    IReadOnlyList<IOrderItem> Items { get; }
    OrderStatus Status { get; }
    OrderType Type { get; set; }
    DateTime CreatedAt { get; }
    DateTime? DeliveredAt { get; }

    void AddItem(IOrderItem item);
    bool RemoveItem(IOrderItem item);
    Domain.ValueObjects.Money CalculateTotalCost();
    void UpdateStatus(OrderStatus newStatus);
    int GetItemCount();
    bool IsEmpty();
}