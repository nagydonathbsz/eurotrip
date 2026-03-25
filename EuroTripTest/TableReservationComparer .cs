using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class TableReservationComparer : IEqualityComparer<TableReservation>
    {
        public bool Equals(TableReservation? x, TableReservation? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.UserId == y?.UserId &&
               x?.TableId == y?.TableId &&
               x?.ResDate == y?.ResDate &&
               x?.ResTime == y?.ResTime &&
               x?.CreatedAt == y?.CreatedAt &&
               x?.Status==y?.Status;
        }

        public int GetHashCode([DisallowNull] TableReservation obj)
        {
            return obj.Id;
        }
    }
}