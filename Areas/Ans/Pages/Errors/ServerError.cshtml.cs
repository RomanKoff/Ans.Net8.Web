using Microsoft.Extensions.Logging;

namespace Ans.Net8.Web.Areas.Ans.Pages.Errors
{

	public class ServerErrorModel(
		ILogger<ServerErrorModel> logger,
		CurrentContext current)
		: AnsErrorPageModel(current)
	{

		public void OnGet()
		{
			Init();
			logger.LogError(
				"server500 | {OriginalPath} | {RefererUri} | {RequestId} | {ExceptionMessage}",
				 OriginalPath, RefererUri, RequestId, ExceptionMessage);
		}

	}

}
