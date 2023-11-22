using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using school_system_api.config;
using school_system_api.Helpers;

namespace school_system_api.Middlewares
{
    public class AuthMiddleware
    {
        private readonly ILogger<AuthMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly Config _config;
        private readonly Token _jwt;
        private readonly Cookies _cookies;

        public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _config = new Config();
            _jwt = new Token();
            _cookies = new Cookies();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (
                    context.Request.Path.Equals("/api/Auth/sign-in", StringComparison.OrdinalIgnoreCase) ||
                    context.Request.Path.Equals("/api/Auth/logout", StringComparison.OrdinalIgnoreCase)
                )
                {
                    await _next.Invoke(context);
                    return;
                }

                _logger.LogInformation("path =====> {path}", context.Request.Path);

                string token = _cookies.Get(context.Request, _config.CookieName);
                _logger.LogInformation("Token: =====> {token}", token);

                if (string.IsNullOrEmpty(token))
                {
                    await AuthorizationHelper.UnauthorizedResponse(context, "Unauthorized");
                    return;
                }

                int? userId = _jwt.ValidateToken(token);

                _logger.LogInformation("decoded user =====> {user}", userId);

                if (userId == null)
                {
                    await AuthorizationHelper.UnauthorizedResponse(context, "Unauthorized");
                    return;
                }

                context.Items["UserId"] = userId;

                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await AuthorizationHelper.UnauthorizedResponse(context, "Failed to authenticate token");
                return;
            }
        }
    }

}