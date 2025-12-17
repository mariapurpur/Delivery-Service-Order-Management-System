using System;

namespace AsianFoodDelivery.Core.Domain.ValueObjects;

public class Money : IEquatable<Money?>
{
    public const string CurrencyCode = "RUB";
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("деньги не могут быть отрицательными((", nameof(amount));

        Amount = Math.Round(amount, 2);
    }

    public static Money operator +(Money left, Money right)
    {
        return new Money(left.Amount + right.Amount);
    }

    public static Money operator -(Money left, Money right)
    {
        var resultAmount = left.Amount - right.Amount;
        if (resultAmount < 0)
            throw new InvalidOperationException("результат не может быть отрицательным");
            
        return new Money(resultAmount);
    }

    public static bool operator ==(Money? left, Money? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Money? left, Money? right)
    {
        return !(left == right);
    }

    public static bool operator >(Money left, Money right)
    {
        if (left is null || right is null) throw new ArgumentNullException(left is null ? nameof(left) : nameof(right));
        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        if (left is null || right is null) throw new ArgumentNullException(left is null ? nameof(left) : nameof(right));
        return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
        if (left is null || right is null) throw new ArgumentNullException(left is null ? nameof(left) : nameof(right));
        return left == right || left > right;
    }

    public static bool operator <=(Money left, Money right)
    {
        if (left is null || right is null) throw new ArgumentNullException(left is null ? nameof(left) : nameof(right));
        return left == right || left < right;
    }

    public bool Equals(Money? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Money);
    }

    public override int GetHashCode()
    {
        return Amount.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Amount} {CurrencyCode}";
    }
}