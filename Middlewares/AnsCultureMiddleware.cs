using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Ans.Net8.Web.Middlewares
{

    public class AnsCultureMiddleware
    {
        private readonly ILogger<AnsCultureMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly string _culture;

        public AnsCultureMiddleware(
            ILogger<AnsCultureMiddleware> logger,
            IConfiguration configuration,
            RequestDelegate next)
        {
            _logger = logger;
            _culture = configuration.GetLibOptions().Culture;
            _next = next;
        }

        public async Task Invoke(
            HttpContext context)
        {
            if (!string.IsNullOrEmpty(_culture))
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(_culture);
                    CultureInfo.CurrentUICulture = new CultureInfo(_culture);
                }
                catch (CultureNotFoundException) { }
            }
            await _next.Invoke(context);
        }
    }

}
