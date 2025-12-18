using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Domain.Entities;


namespace AsianFoodDelivery.Core.Strategies.Interfaces;

public interface IDeliveryStrategy
{
    Money CalculateDeliveryCost(Order order);
    int CalculateDeliveryTime(Order order);
}