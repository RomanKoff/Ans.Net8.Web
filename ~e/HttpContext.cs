using Microsoft.AspNetCore.Http;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
		 * string GetBaseUrl(this HttpContext context);
		 * public static string GetVirtualPath(this HttpContext context);
		 * public static string GetApplicationUrl(this HttpContext context);
		 */


		public static string GetBaseUrl(
			this HttpContext context)
			=> $"{context.Request.Scheme}://{context.Request.Host}";


		public static string GetVirtualPath(
			this HttpContext context)
			=> $"{context.Request.PathBase}/";


		public static string GetApplicationUrl(
			this HttpContext context)
			=> $"{context.GetBaseUrl()}{context.GetVirtualPath()}";

	}

}
