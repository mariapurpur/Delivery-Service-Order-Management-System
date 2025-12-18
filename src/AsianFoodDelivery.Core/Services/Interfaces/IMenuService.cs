using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using System.Collections.Generic;

namespace AsianFoodDelivery.Core.Services.Interfaces;

public interface IMenuService
{
    IReadOnlyList<IMenuItem> GetAvailableItems();
    IMenuItem? GetItemById(Guid id);
    void AddItem(IMenuItem item);
    bool RemoveItem(Guid id);
}