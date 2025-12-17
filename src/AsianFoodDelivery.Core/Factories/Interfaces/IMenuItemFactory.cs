using AsianFoodDelivery.Core.Domain.Menu.Interfaces;

namespace AsianFoodDelivery.Core.Factories.Interfaces;

public interface IMenuItemFactory
{
    IMenuItem CreateMenuItem(MenuItemType itemType, params object[] parameters);
}

public enum MenuItemType
{
    BubbleTea,
    Tea,
    Ramen,
    Sushi,
    BusinessLunch
}