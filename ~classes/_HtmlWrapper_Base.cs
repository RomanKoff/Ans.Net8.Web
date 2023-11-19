using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class _HtmlWrapper_Base(
		IHtmlHelper helper)
		: _Wrapper_Base
	{

		/* readonly properties */

		public IHtmlHelper Helper { get; private set; } = helper;
		public ViewContext ViewContext { get; private set; } = helper.ViewContext;
		public ModelStateDictionary ModelState { get; private set; } = helper.ViewData.ModelState;

	}

}
