using AsianFoodDelivery.Core.Decorators.Interfaces;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Decorators.DishDecorators;

public class DoublePortionDecorator : IMenuItemDecorator
{
    public IMenuItem WrappedItem { get; }
    public Guid Id => WrappedItem.Id;
    public string Name
    {
        get => $"двойная порция {WrappedItem.Name}";
        set => WrappedItem.Name = value;
    }

    private Money? _cachedPrice = null;

    public Money Price
    {
        get
        {
            if (_cachedPrice == null)
            {
                _cachedPrice = new Money(WrappedItem.Price.Amount * 2);
            }
            return _cachedPrice;
        }
        set => WrappedItem.Price = value;
    }

    public bool IsAvailable
    {
        get => WrappedItem.IsAvailable;
        set => WrappedItem.IsAvailable = value;
    }

    public DoublePortionDecorator(IMenuItem wrappedItem)
    {
        WrappedItem = wrappedItem ?? throw new ArgumentNullException(nameof(wrappedItem));
    }

    public Money CalculatePrice()
    {
        var basePrice = WrappedItem.CalculatePrice();
        return new Money(basePrice?.Amount * 2 ?? 0);
    }
}