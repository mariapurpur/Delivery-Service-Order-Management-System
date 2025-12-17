using AsianFoodDelivery.Core.Domain.ValueObjects;
using Xunit;

namespace AsianFoodDelivery.Tests.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void Constructor_AmountNegative()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Money(-1));
        Assert.Contains("отрицательные", exception.Message);
    }

    [Fact]
    public void Constructor_RoundAmoun()
    {
        var amount = 123.456m;

        var money = new Money(amount);

        Assert.Equal(123.46m, money.Amount);
    }

    [Fact]
    public void OperatorAdd_CorrectSum()
    {
        var money1 = new Money(100);
        var money2 = new Money(50);

        var result = money1 + money2;

        Assert.Equal(150m, result.Amount);
    }

    [Fact]
    public void OperatorSubtract_CorrectDifference()
    {
        var money1 = new Money(100);
        var money2 = new Money(50);

        var result = money1 - money2;

        Assert.Equal(50m, result.Amount);
    }

    [Fact]
    public void OperatorSubtract_ResultIsNegative()
    {
        var money1 = new Money(50);
        var money2 = new Money(100);

        var exception = Assert.Throws<InvalidOperationException>(() => money1 - money2);
        Assert.Contains("отрицательные", exception.Message);
    }

    [Fact]
    public void Equals_EqualAmounts()
    {
        var money1 = new Money(100);
        var money2 = new Money(100);

        Assert.True(money1.Equals(money2));
        Assert.True(money1 == money2);
    }

    [Fact]
    public void Equals_DifferentAmounts()
    {
        var money1 = new Money(100);
        var money2 = new Money(50);

        Assert.False(money1.Equals(money2));
        Assert.False(money1 == money2);
    }

    [Fact]
    public void CompareTo_EqualAmounts()
    {
        var money1 = new Money(100);
        var money2 = new Money(100);

        var result = money1.CompareTo(money2);

        Assert.Equal(0, result);
    }

    [Fact]
    public void CompareTo_GreaterAmount()
    {
        var money1 = new Money(150);
        var money2 = new Money(100);

        var result = money1.CompareTo(money2);

        Assert.True(result > 0);
    }
}