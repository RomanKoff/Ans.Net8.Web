using Ans.Net8.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Areas.Ans.Pages.system
{

	public class mappagesResetModel(
		ICurrentContext current)
		: SitePageModel(current)
	{
		public IActionResult OnGet(
			string token,
			string node)
		{
			if (token != _Current.Options.SystemAccessToken)
				return NotFound();
			_Current.Maps.ResetPages(node);
			return Page();
		}
	}

}
