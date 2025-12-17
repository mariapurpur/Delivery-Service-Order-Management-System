using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Orders.Interfaces;

public interface IOrderItem
{
    Money PricePerUnit { get; }
    int Quantity { get; }
    Money TotalPrice { get; }
}