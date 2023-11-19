using Ans.Net8.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ans.Net8.Web.Areas.Errors.Pages
{

	public class HttpErrorsModel(
		ILogger<HttpErrorsModel> logger,
		IConfiguration configuration)
		: SiteErrorModel(logger, configuration)
	{

		public void OnGet(
			int code)
		{
			Init();
			HttpCode = code;
			Logger.LogError(
				"http-{HttpCode} | {OriginalPath} | {RefererUri} | {RequestId} | {ExceptionMessage}",
				HttpCode, OriginalPath, RefererUri, RequestId, ExceptionMessage);
		}

	}

}
