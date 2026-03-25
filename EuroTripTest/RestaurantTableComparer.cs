using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class RestaurantTableComparer : IEqualityComparer<RestaurantTable>
    {
        public bool Equals(RestaurantTable? x, RestaurantTable? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.RestaurantId==y?.RestaurantId &&
               x?.Seats==y?.Seats;
        }

        public int GetHashCode([DisallowNull] RestaurantTable obj)
        {
            return obj.Id;
        }
    }
}