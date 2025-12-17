using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Discount;
using AsianFoodDelivery.Core.Strategies.Interfaces;
using System;

namespace AsianFoodDelivery.Core.Domain.Menu.Combos;

public class BusinessLunch : ComboLunchBase
{
    private readonly IDiscountStrategy _discountStrategy;

    public BusinessLunch(Money basePrice, bool isAvailable = true)
        : base("бизнес-ланч", basePrice, isAvailable)
    {
        _discountStrategy = new ComboDiscountStrategy(20);
    }

    public override Money CalculatePrice()
    {
        var originalPrice = base.CalculatePrice();
        return _discountStrategy.ApplyDiscount(originalPrice);
    }
}