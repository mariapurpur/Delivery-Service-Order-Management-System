using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Domain.ValueObjects;

namespace AsianFoodDelivery.Core.Domain.Menu.Abstractions;

public abstract class MenuItemBase : IMenuItem
{
    public Guid Id { get; protected set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Money Price { get; set; }
    public bool IsAvailable { get; set; }

    protected MenuItemBase(string name, string description, Money price, bool isAvailable = true)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        IsAvailable = isAvailable;
    }

    public virtual Money CalculatePrice()
    {
        return Price;
    }
}