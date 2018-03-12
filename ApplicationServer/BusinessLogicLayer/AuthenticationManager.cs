using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace JoergIsAGeek.Workshop.BusinessLogicLayer {
    public class AuthenticationManager : Manager {

        private IHttpContextAccessor httpContext;

        public AuthenticationManager (IHttpContextAccessor context) {
            httpContext = context;
        }

        public ClaimsPrincipal GetAuthenticatedUser () {
            return httpContext.HttpContext.User;
        }
    }
}