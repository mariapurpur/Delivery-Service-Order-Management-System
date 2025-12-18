using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.States.Interfaces;
using AsianFoodDelivery.Core.Orders.Interfaces;
using AsianFoodDelivery.Core.Domain.Entities;
using System;

namespace AsianFoodDelivery.Core.States;

public class OrderStateContext
{
    private IOrderState _currentState;

    public OrderStateContext(IOrderState initialState)
    {
        _currentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
    }

    public IOrderState CurrentState => _currentState;

    public void SetState(IOrderState newState)
    {
        if (newState == null) throw new ArgumentNullException(nameof(newState));
        _currentState = newState;
    }

    public bool TryAddItem(Order order, IOrderItem item)
    {
        return _currentState.TryAddItem(order, item);
    }

    public bool TryRemoveItem(Order order, IOrderItem item)
    {
        return _currentState.TryRemoveItem(order, item);
    }

    public bool TryUpdateStatus(Order order, OrderStatus newStatus)
    {
        return _currentState.TryUpdateStatus(order, newStatus);
    }
}