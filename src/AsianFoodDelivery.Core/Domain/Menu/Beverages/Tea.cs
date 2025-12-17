using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Beverages;

public class Tea : MenuItemBase
{
    public string Type { get; set; }

    public Tea(string type, Money price, string description = "", bool isAvailable = true)
        : base($"чай ({type})", description, price, isAvailable)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}