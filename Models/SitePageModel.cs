using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ans.Net8.Web.Models
{

	public class SitePageModel
		: PageModel
	{
		internal readonly ICurrentContext _Current;

		public SitePageModel(
			ICurrentContext current)
		{
			_Current = current;
		}
	}

}
