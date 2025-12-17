using AsianFoodDelivery.Core.Domain.Entities;
using Xunit;

namespace AsianFoodDelivery.Tests.Domain.Tests;

public class UserTests
{
    [Fact]
    public void Constructor_NameIsNull()
    {
        var address = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");

        var exception = Assert.Throws<ArgumentNullException>(() => new User(null!, address));
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Constructor_AddressIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new User("Василий Пупкин", null!));
        Assert.Equal("address", exception.ParamName);
    }

    [Fact]
    public void Constructor_UniqueId()
    {
        var address = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");

        var user1 = new User("Василий Пупкин", address);
        var user2 = new User("Василия Пупкина", address);

        Assert.NotEqual(Guid.Empty, user1.Id);
        Assert.NotEqual(Guid.Empty, user2.Id);
        Assert.NotEqual(user1.Id, user2.Id);
    }

    [Fact]
    public void Properties_SetCorrectly()
    {
        var address = new Address("Проспект Славы", "Санкт-Петербург", "Ленинградская область", "10", "5");
        var expectedName = "Василий Пупкин";

        var user = new User(expectedName, address);

        Assert.Equal(expectedName, user.Name);
        Assert.Same(address, user.Address);
    }
}