using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Strategies.Interfaces;

public interface IPricingStrategy
{
    Money CalculatePrice(Money originalPrice);
}