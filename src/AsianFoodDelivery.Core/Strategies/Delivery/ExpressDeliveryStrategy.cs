using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Strategies.Interfaces;

namespace AsianFoodDelivery.Core.Strategies.Delivery;

public class ExpressDeliveryStrategy : IDeliveryStrategy
{
    private readonly Money _baseCost;
    private readonly int _baseTimeMinutes;

    public ExpressDeliveryStrategy(Money baseCost, int baseTimeMinutes)
    {
        _baseCost = baseCost ?? throw new ArgumentNullException(nameof(baseCost));
        _baseTimeMinutes = baseTimeMinutes;
    }

    public Money CalculateDeliveryCost(Order order)
    {
        if (order == null) return new Money(0);
        return _baseCost;
    }

    public int CalculateDeliveryTime(Order order)
    {
        if (order == null) return 0;
        return _baseTimeMinutes;
    }
}