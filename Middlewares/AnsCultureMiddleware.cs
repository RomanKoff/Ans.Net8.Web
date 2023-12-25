using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Ans.Net8.Web.Middlewares
{

	public class AnsCultureMiddleware(
		ILogger<AnsCultureMiddleware> logger,
		IConfiguration configuration,
		RequestDelegate next)
	{

		private readonly ILogger<AnsCultureMiddleware> _logger = logger;
		private readonly RequestDelegate _next = next;
		private readonly string _culture = configuration.GetLibOptions().Culture;


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
