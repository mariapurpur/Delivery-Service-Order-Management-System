using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Interfaces;
using AsianFoodDelivery.Core.Utilities;
using System;

namespace AsianFoodDelivery.Core.Strategies.Discount;

public class TimeDiscountStrategy : IDiscountStrategy
{
    private readonly ITimeService _timeService;
    private readonly decimal _discountPercentage;

    public TimeDiscountStrategy(ITimeService timeService, decimal discountPercentage)
    {
        _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
        if (discountPercentage < 0)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "вы пытаетесь надурить клиентов? -_-");

        _discountPercentage = discountPercentage;
    }

    public Money ApplyDiscount(Money originalPrice)
    {
        var currentTime = _timeService.GetCurrentTime();
        var isAfterNinePM = currentTime.Hour >= 21;

        if (isAfterNinePM)
        {
            var discountMultiplier = (100 - _discountPercentage) / 100;
            var discountedAmount = originalPrice.Amount * discountMultiplier;
            discountedAmount = Math.Round(discountedAmount, 2);
            return new Money(discountedAmount);
        }

        return originalPrice;
    }
}