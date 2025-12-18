using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.States.Interfaces;
using AsianFoodDelivery.Core.Orders.Interfaces;
using AsianFoodDelivery.Core.Domain.Entities;

namespace AsianFoodDelivery.Core.States.OrderStates;

public class NewState : IOrderState
{
    public string Name => "новый";

    public void OnEnter(Order order)
    {
    }

    public void OnExit(Order order)
    {
    }

    public bool TryAddItem(Order order, IOrderItem item)
    {
        if (item == null) return false;
        order.AddItem(item);
        return true;
    }

    public bool TryRemoveItem(Order order, IOrderItem item)
    {
        if (item == null) return false;
        return order.RemoveItem(item);
    }

    public bool TryUpdateStatus(Order order, OrderStatus newStatus)
    {
        if (newStatus == OrderStatus.Confirmed || newStatus == OrderStatus.Cancelled)
        {
            order.UpdateStatus(newStatus);
            return true;
        }
        return false;
    }
}