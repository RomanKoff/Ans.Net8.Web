using Ans.Net8.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Areas.Ans.Pages.system
{

	public class mapnodesResetModel(
		ICurrentContext current)
		: SitePageModel(current)
	{
		public IActionResult OnGet(
			 string token)
		{
			if (token != _Current.Options.SystemAccessToken)
				return NotFound();
			_Current.Maps.ResetNodes();
			return Page();
		}
	}

}
