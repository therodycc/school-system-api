using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using school_system_api.config;

namespace school_system_api.Helpers
{
    public class Token
    {
        private readonly string SecretKey;
        private readonly int ExpirationToken;

        public Token()
        {
            var config = new Config();
            SecretKey = config.SecretKey;
            ExpirationToken = config.ExpirationToken;
        }

        public string SignInToken(string id)
        {
            if (string.IsNullOrEmpty(SecretKey))
                throw new InvalidOperationException("Secret key is not defined");

            var claims = new[] { new Claim(SecretKey, id) };

            var key = new SymmetricSecurityKey(Convert.FromBase64String(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "school_system",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(ExpirationToken),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}