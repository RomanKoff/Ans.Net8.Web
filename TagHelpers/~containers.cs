using Ans.Net8.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <site-container>content</site-container>
	 * <node-container>content</node-container>
	 * <page-container>content</page-container>
	 * <page-container-gap>content</page-container-gap>
	 */



	public class SiteContainerTagHelper(
		ICurrentContext current)
		: _ContainerTagHelper_Proto(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			output.AddAttributeIfPresent("class", _current.Site.ContainerCss, Class);
			output.AddAttributeIfPresent("style", Style);
			output.Content.AppendHtml(output.GetChildContentAsync().Result.GetContent());

		}
	}



	public class NodeContainerTagHelper(
		ICurrentContext current)
		: _ContainerTagHelper_Proto(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			output.AddAttributeIfPresent("class", _current.Node.ContainerCss, Class);
			output.AddAttributeIfPresent("style", Style);
			output.Content.AppendHtml(output.GetChildContentAsync().Result.GetContent());
		}
	}



	public class PageContainerTagHelper(
		ICurrentContext current)
		: _ContainerTagHelper_Proto(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			output.AddAttributeIfPresent("class", _current.Page.CalcCss, Class);
			output.AddAttributeIfPresent("style", Style);
			output.Content.AppendHtml(output.GetChildContentAsync().Result.GetContent());
		}
	}



	public class PageContainerGapTagHelper(
		ICurrentContext current)
		: PageContainerTagHelper(current)
	{
		public bool Stop { get; set; }

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml("</div>");
			output.Content.AppendHtml(output.GetChildContentAsync().Result.GetContent());
			if (Stop)
			{
				output.Content.AppendHtml("<div>");
			}
			else
			{
				output.Content.AppendHtml("<div");
				var class1 = SuppString.Join(null, null, " ", _current.Page.CalcCss, Class);
				if (!string.IsNullOrEmpty(class1))
					output.Content.AppendHtml($" class=\"{class1}\"");
				if (!string.IsNullOrEmpty(Style))
					output.Content.AppendHtml($" style=\"{Style}\"");
				output.Content.AppendHtml(">");
			}
		}
	}



	public class PageContainerOffTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			output.Attributes.Add("style", "color:red;");
			output.Content.AppendHtml("### OBSOLETE TAG. Use the &lt;page-container-gap /> tag ###");
		}
	}



	public class AnsContainerOutsideTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			output.Attributes.Add("style", "color:red;");
			output.Content.AppendHtml("### OBSOLETE TAG. Use the &lt;page-container-gap /> tag ###");
		}
	}



	public abstract class _ContainerTagHelper_Proto(
		ICurrentContext current)
		: TagHelper
	{
		internal readonly ICurrentContext _current = current;

		public string Class { get; set; }
		public string Style { get; set; }
	}

}
