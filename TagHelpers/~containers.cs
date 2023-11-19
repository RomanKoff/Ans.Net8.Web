using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <site-container>content</site-container>
	 * <node-container>content</node-container>
	 * <page-container>content</page-container>
	 * <page-container-off>content</page-container-off>
	 */


	public class SiteContainerTagHelper
		: _ContainerTagHelper_Proto
	{
		public SiteContainerTagHelper(
			ICurrentContext current)
			: base(current)
		{
		}

		internal override string _Class1
			=> string.Join(" ",
				_current.Site.ContainerCss,
				Class);
	}



	public class NodeContainerTagHelper
		: _ContainerTagHelper_Proto
	{
		public NodeContainerTagHelper(
			ICurrentContext current)
			: base(current)
		{
		}

		internal override string _Class1
			=> string.Join(" ",
				_current.Node.ContainerCss,
				Class);
	}



	public class PageContainerTagHelper
		: _ContainerTagHelper_Proto
	{
		public PageContainerTagHelper(
			ICurrentContext current)
			: base(current)
		{
		}

		internal override string _Class1
			=> string.Join(" ",
				_current.Page.ContainerCss,
				_current.Page.AddonCss,
				Class);
	}



	public class PageContainerOffTagHelper
		: PageContainerTagHelper
	{
		public PageContainerOffTagHelper(
			ICurrentContext current)
			: base(current)
		{
		}

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml("</div>");
			output.Content.AppendHtml(output.GetChildContentAsync().Result.GetContent());
			_AppendDiv(output);
		}
	}



	public abstract class _ContainerTagHelper_Proto
		: TagHelper
	{
		internal readonly ICurrentContext _current;
		internal abstract string _Class1 { get; }

		public _ContainerTagHelper_Proto(
			ICurrentContext current)
		{
			_current = current;
		}

		public string Class { get; set; }
		public string Style { get; set; }

		internal void _AppendDiv(TagHelperOutput output)
		{
			output.Content.AppendHtml($"<div");
			if (!string.IsNullOrEmpty(_Class1))
				output.Content.AppendHtml($" class=\"{_Class1}\"");
			if (!string.IsNullOrEmpty(Style))
				output.Content.AppendHtml($" style=\"{Style}\"");
			output.Content.AppendHtml($">");
		}

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			_AppendDiv(output);
			output.Content.AppendHtml(output.GetChildContentAsync().Result.GetContent());
			output.Content.AppendHtml("</div>");
		}
	}



	public class AnsContainerOutsideTagHelper
		: TagHelper
	{
		public AnsContainerOutsideTagHelper()
		{
		}

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml("### ILLEGAL CONTENT ###");
		}
	}

}
