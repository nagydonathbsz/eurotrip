using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class RoomBookingComparer : IEqualityComparer<RoomBooking>
    {
        public bool Equals(RoomBooking? x, RoomBooking? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.RoomId == y?.RoomId &&
               x?.UserId == y?.UserId &&
               x?.CheckIn == y?.CheckIn &&
               x?.CheckOut == y?.CheckOut &&
               x?.Rating == y?.Rating &&
               x?.CreatedAt == y?.CreatedAt &&
               x?.Status == y?.Status;
        }

        public int GetHashCode([DisallowNull] RoomBooking obj)
        {
            return obj.Id;
        }
    }
}