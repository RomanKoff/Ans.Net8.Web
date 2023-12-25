using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
         * string GetMasked(this HtmlString html);
         */


		public static string GetMasked(
			this HtmlString html)
			=> html.ToString().GetHtml2Text();

	}

}
