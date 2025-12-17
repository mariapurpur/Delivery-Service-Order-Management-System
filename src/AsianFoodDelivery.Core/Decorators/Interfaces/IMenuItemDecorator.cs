using AsianFoodDelivery.Core.Domain.Menu.Interfaces;

namespace AsianFoodDelivery.Core.Decorators.Interfaces;

public interface IMenuItemDecorator : IMenuItem
{
    IMenuItem WrappedItem { get; }
}