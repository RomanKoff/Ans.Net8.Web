using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		public static FormHelper GetFormHelper(
			this IHtmlHelper helper)
		{
			var helper1 = new FormHelper(helper);
			return helper1;
		}

	}

}
