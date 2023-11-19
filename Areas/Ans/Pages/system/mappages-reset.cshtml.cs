using Ans.Net8.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Areas.Ans.Pages.system
{

	public class mappages_resetModel
		: SiteSystemModel
	{
		public mappages_resetModel(
			ICurrentContext current)
			: base(current)
		{
		}

		public IActionResult OnGet(
			string token,
			string node)
		{
			var page1 = GetPage(token);
			_Current.Sitemap.MapPagesReset(node);
			return page1;
		}
	}

}
