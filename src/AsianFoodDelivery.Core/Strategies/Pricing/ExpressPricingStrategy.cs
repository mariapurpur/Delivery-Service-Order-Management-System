using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Interfaces;

namespace AsianFoodDelivery.Core.Strategies.Pricing;

public class ExpressPricingStrategy : IPricingStrategy
{
    private readonly decimal _surchargePercentage;

    public ExpressPricingStrategy(decimal surchargePercentage)
    {
        if (surchargePercentage < 0)
            throw new ArgumentOutOfRangeException(nameof(surchargePercentage), "надбавка не может быть отрицательной!");

        _surchargePercentage = surchargePercentage;
    }

    public Money CalculatePrice(Money originalPrice)
    {
        var surchargeMultiplier = (100 + _surchargePercentage) / 100;
        var increasedAmount = originalPrice.Amount * surchargeMultiplier;
        increasedAmount = Math.Round(increasedAmount, 2);

        return new Money(increasedAmount);
    }
}