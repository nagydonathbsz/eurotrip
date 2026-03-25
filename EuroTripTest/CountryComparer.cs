using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class CountryComparer : IEqualityComparer<Country>
    {
        public bool Equals(Country? x, Country? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.Name == y?.Name &&
               x?.PhoneNumber == y?.PhoneNumber &&
               x?.Lang == y?.Lang;
        }

        public int GetHashCode([DisallowNull] Country obj)
        {
            return obj.Id;
        }
    }
}