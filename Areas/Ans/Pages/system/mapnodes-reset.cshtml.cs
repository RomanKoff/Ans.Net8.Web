using Ans.Net8.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Areas.Ans.Pages.system
{

	public class mapnodes_resetModel
		: SiteSystemModel
	{
		public mapnodes_resetModel(
			ICurrentContext current)
			: base(current)
		{
		}

		public IActionResult OnGet(
			 string token)
		{
			var page1 = GetPage(token);
			_Current.Sitemap.MapNodesReset();
			return page1;
		}
	}

}
