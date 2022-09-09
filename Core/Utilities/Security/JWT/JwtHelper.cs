using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        //IConf.. wepapideki appsetting.json dosyasındaki yazdığımı token optionsları okumaya yarıyor.
        public IConfiguration Configuration { get; }
        //tokenoptions--> appsetting.json'daki verilerin karşılığı burada
        private TokenOptions _tokenOptions;
        //token'in 
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            //tokenoptions'ın değerleri --> configurasyondaki tokenoptions değerlerini seç ve tokenoptions classına mapla!!
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //tokenin bitiş tarihi - şu andan itibaren dakika ekle (dk=configurationdan alıyor) 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            //securityKey ifadesini yazdığımz helper aracılığı ile byte[] formatlı olarak atamış olduk,
            //parantez içi olan tokenoptions.securitykey ifademiz appsettings.json'daki mysupersecuritykeyx2 ifadesi
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            //signinghelper ile securityKey ifadesini imzaya dönüştürdük 
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            //jwt için gerekli argümanlar tokenoptionstakiler , kullanıcı , kullanıcının clamleri ve oluşturulmuş imza
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
