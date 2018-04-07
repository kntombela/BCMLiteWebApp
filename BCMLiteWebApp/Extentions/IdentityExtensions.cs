using System.Security.Claims;
using System.Security.Principal;

namespace BCMLiteWebApp.Extentions
{
    public static class IdentityExtensions
    {
        public static string GetFirstName(this IIdentity identity)
        {
            var name = ((ClaimsIdentity)identity).FindFirst("Name");
            // Test for null to avoid issues during local testing
            return (name != null) ? name.Value : string.Empty;
        }

        public static string GetSurname(this IIdentity identity)
        {
            var name = ((ClaimsIdentity)identity).FindFirst("Surname");
            // Test for null to avoid issues during local testing
            return (name != null) ? name.Value : string.Empty;
        }

    }
}