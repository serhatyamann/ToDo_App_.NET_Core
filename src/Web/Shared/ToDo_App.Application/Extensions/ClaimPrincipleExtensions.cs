
using System.Security.Claims;

namespace ToDo_App.Application.Extensions
{
    public static class ClaimPrincipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
