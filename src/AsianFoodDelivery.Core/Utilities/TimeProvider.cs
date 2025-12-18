using System;

namespace AsianFoodDelivery.Core.Utilities;

public interface ITimeService
{
    DateTime GetCurrentTime();
}

public class TimeService : ITimeService
{
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}