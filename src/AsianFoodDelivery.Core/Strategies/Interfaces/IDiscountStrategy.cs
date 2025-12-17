using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Strategies.Interfaces;

public interface IDiscountStrategy
{
    Money ApplyDiscount(Money originalPrice);
}