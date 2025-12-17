using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Discount;
using AsianFoodDelivery.Core.Strategies.Interfaces;
using System;

namespace AsianFoodDelivery.Core.Domain.Menu.Combos;

public class BusinessLunch : ComboLunchBase
{
    private readonly IDiscountStrategy _discountStrategy;

    public BusinessLunch(Money basePrice, string description = "", bool isAvailable = true)
        : base("бизнес-ланч", description, basePrice, isAvailable)
    {
        _discountStrategy = new ComboDiscountStrategy(20);
    }

    public override Money CalculatePrice()
    {
        var originalPrice = base.CalculatePrice();
        if (originalPrice.Amount == 0 && Items.Count > 0)
        {
            originalPrice = Price;
        }
        else if (originalPrice.Amount == 0 && Items.Count == 0)
        {
            originalPrice = Price;
        }

        var discountedPrice = _discountStrategy.ApplyDiscount(originalPrice);
        return discountedPrice ?? new Money(0);
    }
}