using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace JoergIsAGeek.Workshop.ServiceLayer.Middleware {
    public class AuthFilter {
        private readonly RequestDelegate _next;

        public AuthFilter (RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke (HttpContext context) {
            var header = context.Request.Headers.SingleOrDefault (h => h.Key == "X-AuthUser");
            ClaimsIdentity identity;
            if (header.Value.Any ()) {
                identity = new ClaimsIdentity (header.Value.ToString());
            } else {
                identity = new ClaimsIdentity("Udo User");
            }
            context.User = new ClaimsPrincipal (identity);
            await _next (context);
        }
    }
}