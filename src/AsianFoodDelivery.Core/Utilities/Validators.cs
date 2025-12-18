using AsianFoodDelivery.Core.Domain.Entities;
using System.Text.RegularExpressions;

namespace AsianFoodDelivery.Core.Utilities;

public static class Validators
{
    public static bool IsValidUser(User user)
    {
        if (user == null) return false;
        if (string.IsNullOrWhiteSpace(user.Name)) return false;
        if (user.Address == null) return false;

        return true;
    }

    public static bool IsValidOrder(Order order)
    {
        if (order == null) return false;
        if (order.Customer == null) return false;
        if (order.DeliveryAddress == null) return false;
        if (order.IsEmpty()) return false;

        return true;
    }
}