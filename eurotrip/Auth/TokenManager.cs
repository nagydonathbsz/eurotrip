using eurotrip.Modell;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eurotrip.Auth
{

    public class TokenManager
    {
        private string _secretKey = string.Empty;
        private string _issuer = string.Empty;
        private string _audience = string.Empty;
        private Dictionary<int, List<string>> _rolesPermissions = [];
        public List<string> Permissions
        {
            get
            {
                var permissions = new List<string>();
                foreach (var values in _rolesPermissions.Values)
                {
                    permissions.AddRange(values);
                }
                return permissions;
            }
        }

        public TokenManager(ConfigurationManager configuration)
        {
            _secretKey = configuration["Auth:JWT:Key"]!;
            _issuer = configuration["Auth:JWT:Issuer"]!;
            _audience = configuration["Auth:JWT:Audience"]!;

            foreach (var role in configuration.GetSection("Auth:IsAdmin")?.GetChildren() ?? [])
            {
                foreach (var permission in role.GetChildren())
                {
                    if (!string.IsNullOrEmpty(permission.Value))
                    {
                        if (!_rolesPermissions.TryGetValue(int.Parse(role.Key), out var permissionList))
                        {
                            permissionList = [];
                            _rolesPermissions.Add(int.Parse(role.Key), permissionList);
                        }
                        permissionList.Add(permission.Value);
                    }
                }
            }
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Email)
            };
            
            foreach (var permission in _rolesPermissions[user.isAdmin!])
            {
                claims.Add(new Claim("permission", permission));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void RemoveToken(string? header)
        {
            var parts = header?.Split(" ");
            if (parts == null || parts[0] != "Bearer")
            {
                throw new ApplicationException("Érvénytelen token!");
            }
            var token = parts[1];
        }
    }
}