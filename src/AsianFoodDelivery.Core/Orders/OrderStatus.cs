namespace AsianFoodDelivery.Core.Orders;

public enum OrderStatus
{
    New,
    Confirmed,
    Preparing,
    Ready,
    Delivering,
    Delivered,
    Cancelled
}