using eurotrip.Modell;
using System.Diagnostics.CodeAnalysis;

namespace EuroTripTest
{
    internal class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            return x == null && y == null ||
               x?.Id == y?.Id &&
               x?.Name == y?.Name &&
               x?.Email == y?.Email &&
               x?.Phone == y?.Phone &&
               x?.Password == y?.Password &&
               x?.isAdmin == y?.isAdmin &&
               x?.Token==y?.Token;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.Id;
        }
    }
}