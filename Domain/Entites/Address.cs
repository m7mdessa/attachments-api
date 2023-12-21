using Domain.SharedKernel;

namespace Domain.Entites
{
    public class Address : ValueObject
    {
        public string? Street { get; private set; }
        public string? City { get; private set; }
        public string? Country { get; private set; }

        private Address() { }

        public Address(string street, string city, string country)
        {
            Street = street;
            City = city;
            Country = country;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
        }
    }

}

