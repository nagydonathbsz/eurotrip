using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class CityComparer : IEqualityComparer<City>
    {
        public bool Equals(City? x, City? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.Name == y?.Name &&
               x?.ZipCode == y?.ZipCode &&
               x?.CountryId == y?.CountryId;
        }

        public int GetHashCode([DisallowNull] City obj)
        {
            return obj.Id;
        }
    }
}