using Ans.Net8.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <info-version />
	 * <info-display />
	 */



	public class InfoVersionTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "span";
			CustomContent = $"{LibInfo.GetName()} {LibInfo.GetVersion()}{LibInfo.GetDescription().Make(" ({0})")}";
			base.Process(context, output);
		}
	}



	public class InfoDisplayTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml(@"
<div class='debug-display small'>
	<code class='d-block d-sm-none'>XS &lt;576 w100%</code>
	<code class='d-none d-sm-block d-md-none'>SM 576–767 w540</code>
	<code class='d-none d-md-block d-lg-none'>MD 768–991 w720</code>
	<code class='d-none d-lg-block d-xl-none'>LG 992–1199 w960</code>
	<code class='d-none d-xl-block d-xxl-none'>XL 1200–1399 w1140</code>
	<code class='d-none d-xxl-block'>XXL ≥1400 w1320</code>
</div>");
		}
	}

}
