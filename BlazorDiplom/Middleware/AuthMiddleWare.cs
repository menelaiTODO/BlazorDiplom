using IdentityServerCore.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Concurrent;
using System.Net;

namespace BlazorDiplom.Middleware
{
    public class AuthMiddleWare
    {
        public static IDictionary<Guid, (User User, string Password)> AuthInfo { get; private set; } = new ConcurrentDictionary<Guid, (User User, string Password)>();

        private readonly RequestDelegate _next;

        public AuthMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<User> signInManager)
        {
            if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key") && context.Request.Query["key"].First() != null)
            {
                var key = Guid.Parse(context.Request.Query["key"].First()!);
                var info = AuthInfo[key];

                var result = await signInManager.PasswordSignInAsync(info.User, info.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    AuthInfo.Remove(key);
                    context.Response.Redirect("/main");

                    return;
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    return;
                }
            }
            else if (context.Request.Path == "/logOut")
            {
                await signInManager.SignOutAsync();

                context.Response.Redirect("/");
            }
            else
            { 
                await _next.Invoke(context);
            }
        }
    }
}
