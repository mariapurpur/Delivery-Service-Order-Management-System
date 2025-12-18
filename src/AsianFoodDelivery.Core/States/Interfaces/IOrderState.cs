using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Orders.Interfaces;
using AsianFoodDelivery.Core.Domain.Entities;

namespace AsianFoodDelivery.Core.States.Interfaces;

public interface IOrderState
{
    string Name { get; }
    void OnEnter(Order order);
    void OnExit(Order order);
    bool TryAddItem(Order order, IOrderItem item);
    bool TryRemoveItem(Order order, IOrderItem item);
    bool TryUpdateStatus(Order order, OrderStatus newStatus);
}