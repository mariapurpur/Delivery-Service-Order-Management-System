using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Orders.Interfaces;

namespace AsianFoodDelivery.Core.Orders;

public class OrderItem : IOrderItem
{
    public Domain.ValueObjects.Money PricePerUnit { get; }
    public int Quantity { get; }
    public Domain.ValueObjects.Money TotalPrice => new(PricePerUnit.Amount * Quantity);
    public OrderItem(Domain.ValueObjects.Money pricePerUnit, int quantity)
    {
        if (quantity < 1)
            throw new ArgumentException("количество должно быть больше 1!", nameof(quantity));

        PricePerUnit = pricePerUnit ?? throw new ArgumentNullException(nameof(pricePerUnit));
        Quantity = quantity;
    }
}