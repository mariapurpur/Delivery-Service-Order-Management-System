using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Dishes;

public class Sushi : MenuItemBase
{
    public string Type { get; set; }
    public int Pieces { get; set; }

    public Sushi(string type, int pieces, Money price, string description, bool isAvailable = true)
        : base($"{type} суши-сет на {pieces} шт.", description, price, isAvailable)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Pieces = pieces;
    }
}