using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Beverages;

public class BubbleTea : MenuItemBase
{
    public string Flavor { get; set; }
    public bool HasBoba { get; set; }

    public BubbleTea(string flavor, bool hasBoba, Money price, bool isAvailable = true)
        : base($"бабл ти ({flavor})", price, isAvailable)
    {
        Flavor = flavor ?? throw new ArgumentNullException(nameof(flavor));
        HasBoba = hasBoba;
    }
}