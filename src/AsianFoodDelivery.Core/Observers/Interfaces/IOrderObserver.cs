using AsianFoodDelivery.Core.Domain.Entities;

namespace AsianFoodDelivery.Core.Observers.Interfaces;

public interface IOrderObserver
{
    void Update(Order order);
}