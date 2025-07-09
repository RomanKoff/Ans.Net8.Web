using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Ans.Net8.Web.Middlewares
{

	/*
	 *	LibStartup.Use_AnsNet8Web()
	 *		app.UseMiddleware<AnsCultureMiddleware>();
	 */



	public class AnsCultureMiddleware(
		ILogger<AnsCultureMiddleware> logger,
		IConfiguration configuration,
		RequestDelegate next)
	{

		private readonly ILogger<AnsCultureMiddleware> _logger = logger;
		private readonly RequestDelegate _next = next;
		private readonly LibOptions _options = configuration.GetOptions_AnsNet8Web();


		/* functions */


		public async Task Invoke(
			HttpContext context)
		{
			if (!string.IsNullOrEmpty(_options.Culture))
			{
				try
				{
					CultureInfo.CurrentCulture = new CultureInfo(_options.Culture);
					CultureInfo.CurrentUICulture = new CultureInfo(_options.Culture);
				}
				catch (CultureNotFoundException) { }
			}
			await _next.Invoke(context);
		}

	}

}
