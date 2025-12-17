using System;

namespace AsianFoodDelivery.Core.Domain.Entities;

public class Address : IEquatable<Address>
{
    public string Street { get; }
    public string City { get; }
    public string StateOrRegion { get; }
    public string HouseNumber { get; }
    public string ApartmentNumber { get; }

    public Address(string street, string city, string stateOrRegion, string houseNumber, string apartmentNumber)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
        StateOrRegion = stateOrRegion ?? throw new ArgumentNullException(nameof(stateOrRegion));
        HouseNumber = houseNumber ?? throw new ArgumentNullException(nameof(houseNumber));
        ApartmentNumber = apartmentNumber ?? throw new ArgumentNullException(nameof(apartmentNumber));
    }

    public bool Equals(Address? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Street == other.Street &&
               City == other.City &&
               StateOrRegion == other.StateOrRegion &&
               HouseNumber == other.HouseNumber &&
               ApartmentNumber == other.ApartmentNumber;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Address);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, City, StateOrRegion, HouseNumber, ApartmentNumber);
    }
}