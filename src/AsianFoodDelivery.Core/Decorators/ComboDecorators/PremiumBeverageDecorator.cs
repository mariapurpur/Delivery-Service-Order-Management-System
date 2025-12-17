using AsianFoodDelivery.Core.Decorators.Interfaces;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Decorators.ComboDecorators;

public class PremiumBeverageDecorator : IMenuItemDecorator
{
    public IMenuItem WrappedItem { get; }
    public Guid Id => WrappedItem.Id;
    public string Name
    {
        get => $"{WrappedItem.Name} с дополнительным напитком";
        set => WrappedItem.Name = value;
    }

    private readonly IMenuItem _premiumBeverage;
    private Money? _cachedPrice = null;

    public Money Price
    {
        get
        {
            if (_cachedPrice == null)
            {
                _cachedPrice = WrappedItem.Price + _premiumBeverage.Price;
            }
            return _cachedPrice;
        }
        set => WrappedItem.Price = value;
    }

    public bool IsAvailable
    {
        get => WrappedItem.IsAvailable && _premiumBeverage.IsAvailable;
        set => WrappedItem.IsAvailable = value;
    }

    public PremiumBeverageDecorator(IMenuItem wrappedItem, IMenuItem premiumBeverage)
    {
        WrappedItem = wrappedItem ?? throw new ArgumentNullException(nameof(wrappedItem));
        _premiumBeverage = premiumBeverage ?? throw new ArgumentNullException(nameof(premiumBeverage));
    }

    public Money CalculatePrice()
    {
        var basePrice = WrappedItem.CalculatePrice();
        var beveragePrice = _premiumBeverage.CalculatePrice();
        if (basePrice == null) return beveragePrice;
        if (beveragePrice == null) return basePrice;
        return basePrice + beveragePrice;
    }
}