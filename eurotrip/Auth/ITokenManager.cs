using eurotrip.Modell;

namespace eurotrip.Auth
{
    public interface ITokenManager
    {
        List<string> Permissions { get; }
        string GenerateToken(User user);
    }
}
