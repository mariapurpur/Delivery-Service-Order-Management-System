using AsianFoodDelivery.Core.Utilities;
using Xunit;

namespace AsianFoodDelivery.Tests.Services.Tests;

public class TimeServiceTests
{
    [Fact]
    public void GetCurrentTime_Test()
    {
        var timeService = new TimeService();

        var currentTime = timeService.GetCurrentTime();

        Assert.NotEqual(DateTime.MinValue, currentTime);
        Assert.IsType<DateTime>(currentTime);
        var now = DateTime.UtcNow;
        var timeDifference = Math.Abs((now - currentTime).TotalSeconds);
    }
}