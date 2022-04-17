using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace backend.Helpers
{
    public class JwtService
    {
        private string securityKey = "tih is a very secure key no realy, try to brake it :D >:))))";

        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(id.ToString(), null, null, null, expires: DateTime.Today.AddDays(1));

            var jwtSecurityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false

            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;

        }
    }
}
