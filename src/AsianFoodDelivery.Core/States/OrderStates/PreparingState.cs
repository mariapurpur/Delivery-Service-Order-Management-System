using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.States.Interfaces;
using AsianFoodDelivery.Core.Orders.Interfaces;
using AsianFoodDelivery.Core.Domain.Entities;

namespace AsianFoodDelivery.Core.States.OrderStates;

public class PreparingState : IOrderState
{
    public string Name => "готовится";

    public void OnEnter(Order order)
    {
    }

    public void OnExit(Order order)
    {
    }

    public bool TryAddItem(Order order, IOrderItem item)
    {
        return false;
    }

    public bool TryRemoveItem(Order order, IOrderItem item)
    {
        return false;
    }

    public bool TryUpdateStatus(Order order, OrderStatus newStatus)
    {
        if (newStatus == OrderStatus.Ready || newStatus == OrderStatus.Cancelled)
        {
            order.UpdateStatus(newStatus);
            return true;
        }
        return false;
    }
}