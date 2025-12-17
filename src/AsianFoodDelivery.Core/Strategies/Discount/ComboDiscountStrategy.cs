using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Interfaces;

namespace AsianFoodDelivery.Core.Strategies.Discount;

public class ComboDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _discountPercentage;

    public ComboDiscountStrategy(decimal discountPercentage)
    {
        if (discountPercentage < 0)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "вы пытаетесь надурить клиентов?..");

        _discountPercentage = discountPercentage;
    }

    public Money ApplyDiscount(Money originalPrice)
    {
        var discountMultiplier = (100 - _discountPercentage) / 100;
        var discountedAmount = originalPrice.Amount * discountMultiplier;
        discountedAmount = Math.Round(discountedAmount, 2);

        return new Money(discountedAmount);
    }
}