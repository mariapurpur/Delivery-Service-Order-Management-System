using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Dishes;

public class Ramen : MenuItemBase
{
    public string BrothType { get; set; }
    public string Protein { get; set; }

    public Ramen(string brothType, string protein, Money price, string description = "", bool isAvailable = true)
        : base($"рамён ({brothType}, {protein})", description, price, isAvailable)
    {
        BrothType = brothType ?? throw new ArgumentNullException(nameof(brothType));
        Protein = protein ?? throw new ArgumentNullException(nameof(protein));
    }
}