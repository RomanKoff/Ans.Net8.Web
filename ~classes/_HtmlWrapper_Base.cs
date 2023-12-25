using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class _HtmlWrapper_Base(
		IHtmlHelper helper)
		: _Wrapper_Base
	{
		public IHtmlHelper Helper { get; } = helper;
		public ViewContext ViewContext { get; } = helper.ViewContext;
		public ModelStateDictionary ModelState { get; } = helper.ViewData.ModelState;
	}

}
