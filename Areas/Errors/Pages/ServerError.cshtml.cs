using Ans.Net8.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ans.Net8.Web.Areas.Errors.Pages
{

	public class ServerErrorModel(
		ILogger<ServerErrorModel> logger,
		IConfiguration configuration)
		: SiteErrorModel(logger, configuration)
	{

		public void OnGet()
		{
			Init();
			Logger.LogError(
				"server500 | {OriginalPath} | {RefererUri} | {RequestId} | {ExceptionMessage}",
				OriginalPath, RefererUri, RequestId, ExceptionMessage);
		}

	}

}
