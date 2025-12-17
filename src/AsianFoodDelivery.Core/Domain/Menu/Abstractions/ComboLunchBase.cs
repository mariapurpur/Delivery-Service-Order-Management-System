using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsianFoodDelivery.Core.Domain.Menu.Abstractions;

public abstract class ComboLunchBase : MenuItemBase, IComboLunch
{
    protected readonly List<IMenuItem> _items = new();
    public IReadOnlyList<IMenuItem> Items => _items.AsReadOnly();

    protected ComboLunchBase(string name, Money basePrice, bool isAvailable = true)
        : base(name, basePrice, isAvailable)
    {
    }

    public virtual void AddItem(IMenuItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _items.Add(item);
    }

    public virtual bool RemoveItem(IMenuItem item)
    {
        if (item == null) return false;
        return _items.Remove(item);
    }

    public override Money CalculatePrice()
    {
        var totalPrice = new Money(0);
        foreach (var item in _items)
        {
            totalPrice = totalPrice + item.CalculatePrice();
        }
        return totalPrice;
    }
}