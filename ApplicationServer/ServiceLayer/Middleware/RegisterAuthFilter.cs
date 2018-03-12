using System;  
using Microsoft.AspNetCore.Builder;

namespace JoergIsAGeek.Workshop.ServiceLayer.Middleware
{
    public static class RegisterAuthFilterExtension
    {

    public static IApplicationBuilder UseAuthFilter(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthFilter>();
    }

    }
}