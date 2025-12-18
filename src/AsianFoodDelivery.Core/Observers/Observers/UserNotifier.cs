using AsianFoodDelivery.Core.Domain.Entities;
using AsianFoodDelivery.Core.Observers.Interfaces;

namespace AsianFoodDelivery.Core.Observers.Observers;

public class UserNotifier : IOrderObserver
{
    public void Update(Order order)
    {
        if (order == null) return;
        var message = $"уважаемый {order.Customer.Name}, ваш заказ под номером #{order.Id} теперь {order.Status}.";
        Console.WriteLine($"[УВЕДОМЛЕНИЕ ПОЛЬЗОВАТЕЛЮ] {message}");
    }
}