using AsianFoodDelivery.Core.Domain.Entities;
using Xunit;

namespace AsianFoodDelivery.Tests.Domain.Tests;

public class AddressTests
{
    [Fact]
    public void Constructor_StreetIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Address(null!, "город", "регион", "дом", "квартира"));
        Assert.Equal("улица", exception.ParamName);
    }

    [Fact]
    public void Constructor_CityIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Address("улица", null!, "регион", "дом", "квартира"));
        Assert.Equal("город", exception.ParamName);
    }

    [Fact]
    public void Constructor_RegionIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Address("улица", "город", null!, "дом", "квартира"));
        Assert.Equal("регион", exception.ParamName);
    }

    [Fact]
    public void Constructor_HouseIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Address("улица", "город", "регион", null!, "квартира"));
        Assert.Equal("дом", exception.ParamName);
    }

    [Fact]
    public void Constructor_ApartmentIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Address("улица", "город", "регион", "дом", null!));
        Assert.Equal("квартира", exception.ParamName);
    }

    [Fact]
    public void Equals_IdenticalAddresses()
    {
        var address1 = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        var address2 = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");

        Assert.True(address1.Equals(address2));
        Assert.True(address1 == address2);
    }

    [Fact]
    public void Equals_DifferentAddresses()
    {
        var address1 = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        var address2 = new Address("Альпийский переулок", "Санкт-Петербург", "Ленинградская область", "15", "5");

        Assert.False(address1.Equals(address2));
        Assert.False(address1 == address2);
    }

    [Fact]
    public void GetHashCode_EqualAddresses()
    {
        var address1 = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        var address2 = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");

        var hash1 = address1.GetHashCode();
        var hash2 = address2.GetHashCode();

        Assert.Equal(hash1, hash2);
    }
}