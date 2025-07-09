using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ans.Net8.Web
{

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[IgnoreAntiforgeryToken]
	public class AnsErrorPageModel(
		CurrentContext current)
		: _AnsPageModel_Base(current)
	{

		/* properties */


		public Exception Exception { get; set; }
		public string RequestId { get; set; }
		public string ExceptionMessage { get; set; }
		public string OriginalPath { get; set; }
		public Uri RefererUri { get; set; }
		public int HttpCode { get; set; }
		public bool ShowInfo { get; set; }


		/* readonly properties */


		public bool HasRequestId
			=> !string.IsNullOrEmpty(RequestId);

		public bool HasExceptionMessage
			=> !string.IsNullOrEmpty(ExceptionMessage);

		public bool HasOriginalPath
			=> !string.IsNullOrEmpty(OriginalPath);

		public bool HasRefererUri
			=> RefererUri != null;


		/* methods */


		public virtual void Init()
		{
			var f1 = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
			var f2 = HttpContext.Features.Get<IExceptionHandlerFeature>();
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
			RefererUri = Request.GetTypedHeaders().Referer;
			OriginalPath = f1?.OriginalPath;
			Exception = f2?.Error;
			ExceptionMessage = f2?.Error.Message;
			ShowInfo = Options.Errors != null && Options.Errors.ShowInfo;
		}

	}

}
