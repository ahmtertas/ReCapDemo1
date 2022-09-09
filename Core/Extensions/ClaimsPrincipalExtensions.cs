using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //bir kişinin claimlerini ulaşmak-okumak için yazılmış bir extension'dır.
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            //? ifadesi null olabileceğini ifade etmektedir. 
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
