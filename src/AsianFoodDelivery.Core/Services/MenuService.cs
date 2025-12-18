using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AsianFoodDelivery.Core.Services;

public class MenuService : IMenuService
{
    private readonly List<IMenuItem> _menuItems = new();

    public IReadOnlyList<IMenuItem> GetAvailableItems()
    {
        return _menuItems.Where(item => item.IsAvailable).ToList();
    }

    public IMenuItem? GetItemById(Guid id)
    {
        return _menuItems.FirstOrDefault(item => item.Id == id);
    }

    public void AddItem(IMenuItem? item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _menuItems.Add(item);
    }

    public bool RemoveItem(Guid id)
    {
        var item = GetItemById(id);
        if (item == null) return false;
        return _menuItems.Remove(item);
    }
}