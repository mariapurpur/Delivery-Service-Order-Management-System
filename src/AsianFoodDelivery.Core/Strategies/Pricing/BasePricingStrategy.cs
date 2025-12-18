using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Interfaces;

namespace AsianFoodDelivery.Core.Strategies.Pricing;

public class BasePricingStrategy : IPricingStrategy
{
    public Money CalculatePrice(Money originalPrice)
    {
        return originalPrice ?? new Money(0);
    }
}