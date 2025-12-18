using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Orders.Interfaces;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;

namespace AsianFoodDelivery.Core.Orders;

public class OrderItem : IOrderItem
{
    public IMenuItem MenuItem { get; }
    public int Quantity { get; }
    public Money PricePerUnit => MenuItem.Price;
    public Domain.ValueObjects.Money TotalPrice => new(PricePerUnit.Amount * Quantity);
    public OrderItem(IMenuItem menuItem, int quantity)
    {
        if (quantity < 1)
            throw new ArgumentException("количество должно быть больше 1!", nameof(quantity));

        MenuItem = menuItem ?? throw new ArgumentNullException(nameof(menuItem));
        Quantity = quantity;
    }
}