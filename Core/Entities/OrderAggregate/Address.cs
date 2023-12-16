namespace Core.Entities.OrderAggregate;

public class Address
{
    public Address()
    {
    }

    public Address(
        string firstName,
        string lastName,
        string street,
        string city,
        string state,
        string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Street { get; init; }

    public string City { get; init; }

    public string State { get; init; }

    public string ZipCode { get; init; }
}
