using System;

namespace AsianFoodDelivery.Core.Domain.Entities;

public class User
{
    public Guid Id { get; }
    public string Name { get; set; }
    public Address Address { get; set; }

    public User(string name, string email, Address address)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }
}