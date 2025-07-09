using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web
{

	public class AnsSystemPageModel(
		CurrentContext current)
		: _AnsPageModel_Base(current)
	{

		public virtual IActionResult OnGet()
		{
			var token1 = Current.QueryString.GetString("token");
			if (token1 == Options.SystemToken)
				return null;
			return NotFound();
		}

	}

}
