using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Ans.Net8.Web
{

	public static partial class _e_View
	{

		/* functions */


		public static string GetViewName(
			this IView view)
		{
			return Path.GetFileNameWithoutExtension(
				view.Path[view.Path.LastIndexOf('/')..]);
		}


		public static string GetViewName(
			this IRazorPage view)
		{
			return Path.GetFileNameWithoutExtension(
				view.Path[view.Path.LastIndexOf('/')..]);
		}

	}

}
