using AsianFoodDelivery.Core.Orders;
using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Orders.Interfaces;
using System.Collections.Generic;

namespace AsianFoodDelivery.Core.Services.Interfaces;

public interface IOrderService
{
    Order CreateOrder(User customer, Address deliveryAddress, OrderType type);
    Order? GetOrderById(Guid id);
    IReadOnlyList<Order> GetAllOrders();
    void UpdateOrderStatus(Guid orderId, OrderStatus newStatus);
    void AddItemToOrder(Guid orderId, IOrderItem item);
    bool RemoveItemFromOrder(Guid orderId, IOrderItem item);
}