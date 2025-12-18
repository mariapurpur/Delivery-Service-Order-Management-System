using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Interfaces;

namespace AsianFoodDelivery.Core.Strategies.Pricing;

public class ComboPricingStrategy : IPricingStrategy
{
    private readonly decimal _discountPercentage;

    public ComboPricingStrategy(decimal discountPercentage)
    {
        if (discountPercentage < 0)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "вы пытаетесь надурить клиентов? -_-");

        _discountPercentage = discountPercentage;
    }

    public Money CalculatePrice(Money originalPrice)
    {
        var discountMultiplier = (100 - _discountPercentage) / 100;
        var discountedAmount = originalPrice.Amount * discountMultiplier;
        discountedAmount = Math.Round(discountedAmount, 2);

        return new Money(discountedAmount);
    }
}