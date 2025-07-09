using Ans.Net8.Common;
using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Ans.Net8.Web
{

	public static partial class _e_ViewContext
	{

		/* functions */


		public async static Task<string> RenderViewAsync(
			this ViewContext context,
			string viewName,
			object model)
		{
			var engine1 = context.HttpContext.RequestServices
				.GetRequiredService<IViewRenderService>();
			return await engine1.RenderViewToStringAsync(viewName, model);
		}


		public static string GetRouteValueAsString(
			this ViewContext context,
			string key)
		{
			return context.HttpContext.GetRouteValue(key)?.ToString();
		}


		public static string GetRouteValueAsString(
			this ViewContext context,
			string key,
			string defaultValue)
		{
			return context.GetRouteValueAsString(key) ?? defaultValue;
		}


		public static int? GetRouteValueAsInt(
			this ViewContext context,
			string key)
		{
			return context.GetRouteValueAsString(key)?.ToInt();
		}


		public static int GetRouteValueAsInt(
			this ViewContext context,
			string key,
			int defaultValue)
		{
			return context.GetRouteValueAsInt(key) ?? defaultValue;
		}


		public static bool GetRouteValueAsBool(
			this ViewContext context,
			string key)
		{
			var item1 = context.GetRouteValueAsString(key);
			return item1 != null && item1.ToBool();
		}

	}

}
