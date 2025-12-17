using System.Collections.Generic;

namespace AsianFoodDelivery.Core.Domain.Menu.Interfaces;

public interface IComboLunch : IMenuItem
{
    IReadOnlyList<IMenuItem> Items { get; }
    void AddItem(IMenuItem item);
    bool RemoveItem(IMenuItem item);
}