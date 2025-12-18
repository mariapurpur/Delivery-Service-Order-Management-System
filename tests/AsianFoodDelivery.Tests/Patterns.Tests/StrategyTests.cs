using AsianFoodDelivery.Core.Domain.ValueObjects;
using AsianFoodDelivery.Core.Strategies.Discount;
using AsianFoodDelivery.Core.Strategies.Interfaces;
using AsianFoodDelivery.Core.Strategies.Pricing;
using AsianFoodDelivery.Core.Utilities;
using Moq;
using Xunit;

namespace AsianFoodDelivery.Tests.Patterns.Tests;

public class StrategyTests
{
    [Fact]
    public void BasePricingStrategy_OriginalPrice()
    {
        var originalPrice = new Money(100);
        var strategy = new BasePricingStrategy();

        var result = strategy.CalculatePrice(originalPrice);

        Assert.Equal(originalPrice, result);
    }

    [Fact]
    public void ComboPricingStrategy_Discount()
    {
        var originalPrice = new Money(100);
        var strategy = new ComboPricingStrategy(20); // 20%
        
        var result = strategy.CalculatePrice(originalPrice);

        Assert.Equal(80m, result.Amount);
    }

    [Fact]
    public void TimeDiscountStrategy_Discount9PM()
    {
        var originalPrice = new Money(100);
        var mockTimeService = new Mock<ITimeService>();
        mockTimeService.Setup(ts => ts.GetCurrentTime()).Returns(new DateTime(2023, 1, 1, 22, 0, 0)); // 22:00
        var strategy = new TimeDiscountStrategy(mockTimeService.Object, 10); // 10%

        var result = strategy.ApplyDiscount(originalPrice);

        Assert.Equal(90m, result.Amount);
    }

    [Fact]
    public void TimeDiscountStrategy_NoDiscount9PM()
    {
        var originalPrice = new Money(100);
        var mockTimeService = new Mock<ITimeService>();
        mockTimeService.Setup(ts => ts.GetCurrentTime()).Returns(new DateTime(2023, 1, 1, 20, 0, 0)); // 20:00
        var strategy = new TimeDiscountStrategy(mockTimeService.Object, 10); // 10%

        var result = strategy.ApplyDiscount(originalPrice);

        Assert.Equal(100m, result.Amount);
    }

    [Fact]
    public void ComboDiscountStrategy_DiscountLunch()
    {
        var originalPrice = new Money(100);
        var mockTimeService = new Mock<ITimeService>();
        mockTimeService.Setup(ts => ts.GetCurrentTime()).Returns(new DateTime(2023, 1, 1, 13, 0, 0)); // 13:00
        var strategy = new ComboDiscountStrategy(15); // 15%

        var result = strategy.ApplyDiscount(originalPrice);

        Assert.Equal(85m, result.Amount);
    }

    [Fact]
    public void ComboDiscountStrategy_NoDiscount()
    {
        var originalPrice = new Money(100);
        var mockTimeService = new Mock<ITimeService>();
        mockTimeService.Setup(ts => ts.GetCurrentTime()).Returns(new DateTime(2023, 1, 1, 15, 0, 0)); // 15:00
        var strategy = new ComboDiscountStrategy(15); // 15%

        var result = strategy.ApplyDiscount(originalPrice);

        Assert.Equal(85m, result.Amount);
    }
}