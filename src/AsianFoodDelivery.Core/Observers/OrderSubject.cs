using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Observers.Interfaces;
using System.Collections.Generic;

namespace AsianFoodDelivery.Core.Observers;

public class OrderSubject : IOrderSubject
{
    private readonly List<IOrderObserver> _observers = new();
    private readonly Order _order;

    public OrderSubject(Order order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }
    
    public void Attach(IOrderObserver observer)
    {
        if (observer == null) throw new ArgumentNullException(nameof(observer));
        _observers.Add(observer);
    }

    public void Detach(IOrderObserver observer)
    {
        if (observer == null) return;
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_order);
        }
    }
}