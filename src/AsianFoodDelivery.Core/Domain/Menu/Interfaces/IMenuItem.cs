using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Interfaces;

public interface IMenuItem
{
    Guid Id { get; }
    string Name { get; set; }
    Money Price { get; set; }
    bool IsAvailable { get; set; }
    Money CalculatePrice();
}