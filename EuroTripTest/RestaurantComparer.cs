using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class RestaurantComparer : IEqualityComparer<Restaurant>
    {
        public bool Equals(Restaurant? x, Restaurant? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.Name == y?.Name &&
               x?.Address == y?.Address &&
               x?.Image == y?.Image &&
               x?.Phone == y?.Phone &&
               x?.CityId == y?.CityId;
        }

        public int GetHashCode([DisallowNull] Restaurant obj)
        {
            return obj.Id;
        }
    }
}