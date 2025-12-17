using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Beverages;

public class GreenTea : MenuItemBase
{
    public string Type { get; set; }

    public GreenTea(string type, Money price, bool isAvailable = true)
        : base($"чай ({type})", price, isAvailable)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}