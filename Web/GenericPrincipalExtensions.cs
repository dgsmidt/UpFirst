using System.Security.Claims;
using System.Security.Principal;

namespace Web
{
    public static class GenericPrincipalExtensions
    {
        public static string Nome(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type == "Nome")
                        return claim.Value;
                }
                return "";
            }
            else
                return "";
        }
    }
}
