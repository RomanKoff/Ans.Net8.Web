using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <info-display />
	 * <info-ansnet8web />
	 */



	public class InfoDisplayTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml(
				"<div class='small'>" +
				"<code class='debug-display d-block d-sm-none'><h6 class='mb-0 mt-2'>XS</b> &lt;576(c:auto)</code>" +
				"<code class='debug-display d-none d-sm-block d-md-none'><h6 class='mb-0 mt-2'>SM</b> ≥576(c:540)</code>" +
				"<code class='debug-display d-none d-md-block d-lg-none'><h6 class='mb-0 mt-2'>MD</b> ≥720(c:720)</code>" +
				"<code class='debug-display d-none d-lg-block d-xl-none'><h6 class='mb-0 mt-2'>LG</b> ≥960(c:960)</code>" +
				"<code class='debug-display d-none d-xl-block d-xxl-none'><h6 class='mb-0 mt-2'>XL</b> ≥1200(c:1140)</code>" +
				"<code class='debug-display d-none d-xxl-block'><h6 class='mb-0 mt-2'>XXL</b> ≥1400(c:1320)</code>" +
				"</div>");
		}
	}



	public class InfoAnsnet8webTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml($"{Common.LibInfo.GetName()} {Common.LibInfo.GetVersion()} ({Common.LibInfo.GetDescription()})<br/>");
			output.Content.AppendHtml($"{LibInfo.GetName()} {LibInfo.GetVersion()} ({LibInfo.GetDescription()})<br/>");
		}
	}

}
