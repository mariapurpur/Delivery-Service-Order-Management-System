using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Observers.Interfaces;

namespace AsianFoodDelivery.Core.Observers.Observers;

public class RestaurantNotifier : IOrderObserver
{
    public void Update(Order order)
    {
        if (order == null) return;
        var message = $"заказ под номером #{order.Id} успешно перешел в статус {order.Status}. подтвердите статус.";
        Console.WriteLine($"[УВЕДОМЛЕНИЕ РЕСТОРАНУ] {message}");
    }
}