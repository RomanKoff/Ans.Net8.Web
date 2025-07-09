using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <site-container>content</site-container>
	 * <node-container>content</node-container>
	 * <page-container>content</page-container>
	 * <page-container-body>content</page-container-body>
	 * <admin-container free="false">content</admin-container>
	 */



	public class SiteContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			ClassBefore = Current.Site.ContainerClasses;
			base.Process(context, output);
		}
	}



	public class NodeContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			ClassBefore = Current.Node.ContainerClasses;
			base.Process(context, output);
		}
	}



	public class PageContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			ClassBefore = Current.Page.ContainerClasses;
			base.Process(context, output);
		}
	}



	public class PageContainerBodyTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			if (Current.Page.UseDesignerMode)
			{
				output.TagName = null;
				return;
			}
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			ClassBefore = Current.Page.ContainerClasses;
			base.Process(context, output);
		}
	}



	public class AdminContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{

		private const string Free_AttributeName = "free";


		/* properties */


		[HtmlAttributeName(Free_AttributeName)]
		public bool IsFree { get; set; } = false;


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagName = null;
			if (!Current.Network.IsAdmin)
			{
				output.Content.Clear();
				return;
			}
			if (!IsFree)
			{
				output.TagMode = TagMode.StartTagAndEndTag;
				output.TagName = "div";
				Class = "bg-warning-subtle";
			}
			base.Process(context, output);
		}
	}

}
