using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Orders.Interfaces;
using AsianFoodDelivery.Core.Utilities;
using AsianFoodDelivery.Core.Strategies.Discount;
using AsianFoodDelivery.Core.Services.Interfaces;
using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Strategies.Pricing;
using AsianFoodDelivery.Core.Strategies.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AsianFoodDelivery.Core.Services;

public class PricingService : IPricingService
{
    private readonly ITimeService _timeService;

    public PricingService(ITimeService timeService)
    {
        _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
    }

    public Money CalculateTotalPrice(Order order)
    {
        if (order == null) return new Money(0);

        var totalPrice = new Money(0);

        foreach (var item in order.Items)
        {
            var itemPrice = CalculateItemPrice(item);
            totalPrice = totalPrice + itemPrice;
        }

        totalPrice = ApplyGlobalDiscounts(totalPrice, order);

        return totalPrice;
    }

    private Money CalculateItemPrice(IOrderItem item)
    {
        var basePriceAmount = item.PricePerUnit.Amount * item.Quantity;
        var basePrice = new Money(basePriceAmount);
        
        IPricingStrategy strategy = GetPricingStrategyForItem(item);

        return strategy.CalculatePrice(basePrice);
    }

    private IPricingStrategy GetPricingStrategyForItem(IOrderItem item)
    {
        return new BasePricingStrategy();
    }

    private Money ApplyGlobalDiscounts(Money totalPrice, Order order)
    {
        var timeDiscountStrategy = new TimeDiscountStrategy(_timeService, 10); // 10%
        return timeDiscountStrategy.ApplyDiscount(totalPrice);
    }
}