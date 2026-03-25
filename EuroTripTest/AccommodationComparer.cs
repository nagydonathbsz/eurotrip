using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class AccommodationComparer : IEqualityComparer<Accommodation>
    {
        public bool Equals(Accommodation? x, Accommodation? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.Name == y?.Name &&
               x?.Address == y?.Address &&
               x?.Image == y?.Image &&
               x?.Phone == y?.Phone &&
               x?.CityId == y?.CityId;
        }

        public int GetHashCode([DisallowNull] Accommodation obj)
        {
            return obj.Id;
        }
    }
}