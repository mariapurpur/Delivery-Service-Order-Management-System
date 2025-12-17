using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Factories.Interfaces;
using AsianFoodDelivery.Core.Orders;

namespace AsianFoodDelivery.Core.Factories;

public class OrderFactory : IOrderFactory
{
    public Order CreateOrder(User customer, Address deliveryAddress, OrderType orderType)
    {
        var order = new Order(customer, deliveryAddress, orderType);
        // потенциал на дополнение
        return order;
    }
}