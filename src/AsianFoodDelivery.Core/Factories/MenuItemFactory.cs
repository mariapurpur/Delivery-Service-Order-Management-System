using AsianFoodDelivery.Core.Domain.Menu.Beverages;
using AsianFoodDelivery.Core.Domain.Menu.Combos;
using AsianFoodDelivery.Core.Domain.Menu.Dishes;
using AsianFoodDelivery.Core.Domain.Menu.Interfaces;
using AsianFoodDelivery.Core.Factories.Interfaces;
using System.Reflection;

namespace AsianFoodDelivery.Core.Factories;

public class MenuItemFactory : IMenuItemFactory
{
    public IMenuItem CreateMenuItem(MenuItemType itemType, params object[] parameters)
    {

        Type concreteType = itemType switch
        {
            MenuItemType.BubbleTea => typeof(BubbleTea),
            MenuItemType.Tea => typeof(Tea),
            MenuItemType.Ramen => typeof(Ramen),
            MenuItemType.Sushi => typeof(Sushi),
            MenuItemType.BusinessLunch => typeof(BusinessLunch),
            _ => throw new ArgumentException($"такого у нас в меню нет...", nameof(itemType))
        };

        try
        {
            var menuItem = Activator.CreateInstance(concreteType, parameters);
            if (menuItem is IMenuItem item)
            {
                return item;
            }
            else
            {
                throw new InvalidOperationException($"при внесении предмета произошла ошибка!");
            }
        }
        catch (TargetInvocationException ex) when (ex.InnerException != null)
        {
            throw ex.InnerException;
        }
    }
}