using AsianFoodDelivery.Core.Domain.Menu.Abstractions;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Dishes;

public class Sushi : MenuItemBase
{
    public string Type { get; set; }
    public int Pieces { get; set; }

    public Sushi(string type, int pieces, Money price, bool isAvailable = true)
        : base($"{type} суши-сет на {pieces} шт.", price, isAvailable)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Pieces = pieces;
    }
}