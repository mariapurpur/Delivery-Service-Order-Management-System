using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Domain.Entities;

namespace AsianFoodDelivery.Core.Services.Interfaces;

public interface IPricingService
{
    Money CalculateTotalPrice(Order order);
}