using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using API.Models;
using Microsoft.Extensions.Options;
using API.Helpers;
namespace API.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new List<Claim>
         {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id),
                 identity.FindFirst(System.Security.Claims.ClaimsIdentity.DefaultNameClaimType)

                 //identity.FindFirst(System.Security.Claims.ClaimsIdentity.DefaultRoleClaimType)
             };
            claims.AddRange(identity.FindAll(System.Security.Claims.ClaimsIdentity.DefaultRoleClaimType));
            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, List<string> userRoles, string id)
        {
            var claims = new List<Claim>
                {
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"),
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, "Manager")

                };
            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }
            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
            //return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            //{
            //    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
            //    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess)
            //});
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
