using Microsoft.AspNetCore.Mvc.Rendering;
using System.Resources;

namespace Ans.Net8.Web.Forms
{

	public static partial class _e
	{

		/* functions */


		public static FormHelper AppendFormHelper(
			this IHtmlHelper helper,
			string name,
			params ResourceManager[] resources)
		{
			var helper1 = new FormHelper(helper, resources);
			helper.ViewData[name] = helper1;
			return helper1;
		}


		public static FormHelper GetFormHelper(
			this IHtmlHelper helper,
			string name)
		{
			return helper.ViewData[name] as FormHelper;
		}

	}

}
