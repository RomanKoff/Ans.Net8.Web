using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ans.Net8.Web.Models
{

	public class SitePageModel(
		ICurrentContext current)
		: PageModel
	{
		internal readonly ICurrentContext _Current = current;
	}

}
