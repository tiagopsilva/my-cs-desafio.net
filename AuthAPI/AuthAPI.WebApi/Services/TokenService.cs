using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Services.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace AuthAPI.WebApi.Services
{
    public class AuthService : ITokenService
    {
        private const string _issuer = "http://localhost:53644";
        private const string Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";

        public string GenerateToken(Usuario usuario)
        {
            var ticket = GenerateTicket(usuario);

            var accessToken = Protect(ticket);

            return accessToken;
        }

        public static AuthenticationTicket GenerateTicket(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("Usuário inválido");

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim("sub", usuario.Id.ToString("N")));
            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nome));

            var props = new AuthenticationProperties(new Dictionary<string, string>())
            {
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30)
            };

            var ticket = new AuthenticationTicket(identity, props);
            return ticket;
        }

        public static string Protect(AuthenticationTicket data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var keyByteArray = TextEncodings.Base64Url.Decode(Base64Secret);
            var signingKey = new HmacSigningCredentials(keyByteArray);
            var issued = data.Properties.IssuedUtc?.UtcDateTime;
            var expires = data.Properties.ExpiresUtc?.UtcDateTime;

            var token = new JwtSecurityToken(_issuer, null, data.Identity.Claims, issued, expires, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }
    }
}