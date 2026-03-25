using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class RoomComparer : IEqualityComparer<Room>
    {
        public bool Equals(Room? x, Room? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.AccommodationId == y?.AccommodationId;
        }

        public int GetHashCode([DisallowNull] Room obj)
        {
            return obj.Id;
        }
    }
}