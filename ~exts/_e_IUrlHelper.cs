using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web
{

	public static partial class _e_IUrlHelper
	{

		/* functions */


		/// <summary>
		/// Generates a fully qualified URL to an action method by using
		/// the specified action name, controller name and route values
		/// </summary>
		public static string AbsoluteAction(
			this IUrlHelper url,
			string actionName,
			string controllerName,
			object routeValues = null)
		{
			return url.Action(
				actionName, controllerName,
				routeValues, url.ActionContext.HttpContext.Request.Scheme);
		}


		/// <summary>
		/// Generates a fully qualified URL to the specified content
		/// by using the specified content path. Converts a virtual
		/// (relative) path to an application absolute path
		/// </summary>
		public static string AbsoluteContent(
			this IUrlHelper url,
			string contentPath)
		{
			var r1 = url.ActionContext.HttpContext.Request;
			var uri1 = new Uri($"{r1.Scheme}://{r1.Host.Value}");
			return new Uri(uri1, url.Content(contentPath)).ToString();
		}


		/// <summary>
		/// Generates a fully qualified URL to the specified route
		/// by using the route name and route values
		/// </summary>
		public static string AbsoluteRouteUrl(
			this IUrlHelper url,
			string routeName,
			object routeValues = null)
		{
			return url.RouteUrl(
				routeName, routeValues,
				url.ActionContext.HttpContext.Request.Scheme);
		}

	}

}
