using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Models
{

	public class SiteSystemModel
		: SitePageModel
	{
		public SiteSystemModel(
			ICurrentContext current)
			: base(current)
		{
		}

		public IActionResult GetPage(
			 string token)
		{
			if (token != _Current.Options.SystemAccessToken)
				return NotFound();
			return Page();
		}
	}

}
