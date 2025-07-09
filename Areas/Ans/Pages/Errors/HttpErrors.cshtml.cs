using Microsoft.Extensions.Logging;

namespace Ans.Net8.Web.Areas.Ans.Pages.Errors
{

	public class HttpErrorsModel(
		ILogger<HttpErrorsModel> logger,
		CurrentContext current)
		: AnsErrorPageModel(current)
	{

		public void OnGet(
			int code)
		{
			Init();
			HttpCode = code;
			logger.LogError(
				"http-{HttpCode} | {OriginalPath} | {RefererUri} | {RequestId} | {ExceptionMessage}",
				HttpCode, OriginalPath, RefererUri, RequestId, ExceptionMessage);
		}

	}

}
