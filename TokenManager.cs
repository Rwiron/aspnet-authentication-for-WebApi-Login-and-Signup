using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Mis_Management_system
{
    public class TokenManager
    {
        // creating the secret key the reason we use jwt token is that we want to secure our apis
        // so whenever we are going to hit that api it will check that jwt token refer to the correct
        // if not it will not working otherwise it will crash it a best pratice in managing apis security
        // and if it correct user will pass if not it will give user unauthorized access
        // you have to create it for you self randomly
        public static string secretKey = "dsdsfwoeudfwoeifhdusfuhiewuwedcuhsdcfwejndwusdcwjkefcew123460594342dwd";
        public static string GenerateToken(string email,string role)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email), new Claim(ClaimTypes.Role, role) }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}