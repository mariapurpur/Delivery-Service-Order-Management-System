using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Orders;

namespace AsianFoodDelivery.Core.Factories.Interfaces;

public interface IOrderFactory
{
    Order CreateOrder(User customer, Address deliveryAddress, OrderType orderType);
}