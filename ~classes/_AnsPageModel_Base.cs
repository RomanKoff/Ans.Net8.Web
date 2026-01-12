using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ans.Net8.Web
{

	public class _AnsPageModel_Base(
		CurrentContext current)
		: PageModel
	{
		public readonly CurrentContext Current = current;
		public readonly LibWebOptions Options = current.Configuration.GetLibWebOptions();
	}

}
