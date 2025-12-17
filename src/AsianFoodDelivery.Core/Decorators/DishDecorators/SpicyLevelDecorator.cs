using AsianFoodDelivery.Core.Decorators.Interfaces;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Decorators.DishDecorators;

public class SpicyLevelDecorator : IMenuItemDecorator
{
    public IMenuItem WrappedItem { get; }
    public Guid Id => WrappedItem.Id;
    public string Name
    {
        get => $"{WrappedItem.Name} (уровень остроты: {_spicyLevel})";
        set => WrappedItem.Name = value;
    }

    private readonly int _spicyLevel;
    private readonly Money _extraCost;
    private Money? _cachedPrice = null;

    public Money Price
    {
        get
        {
            if (_cachedPrice == null)
            {
                _cachedPrice = WrappedItem.Price + _extraCost;
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

    public SpicyLevelDecorator(IMenuItem wrappedItem, int spicyLevel, Money extraCost)
    {
        WrappedItem = wrappedItem ?? throw new ArgumentNullException(nameof(wrappedItem));
        if (spicyLevel < 1 || spicyLevel > 10)
            throw new ArgumentOutOfRangeException(nameof(spicyLevel), "уровень остроты должен быть оценён по шкале от 1 до 10!");
        _spicyLevel = spicyLevel;
        _extraCost = extraCost ?? throw new ArgumentNullException(nameof(extraCost));
    }

    public Money CalculatePrice()
    {
        var basePrice = WrappedItem.CalculatePrice();
        if (basePrice == null) return _extraCost;
        return basePrice + _extraCost;
    }
}