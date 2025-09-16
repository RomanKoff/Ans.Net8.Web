using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class LinkBuilder
	{

		/* ctors */


		public LinkBuilder(
			string href,
			string title,
			bool isDisabled = false)
		{
			Href = href;
			InnerHtml = SuppTypograph.GetTypografMin(title);
			IsDisabled = isDisabled; // || string.IsNullOrEmpty(href);
		}


		//public LinkBuilder(
		//	string title)
		//	: this(null, title)
		//{
		//}


		/* properties */


		public string Id { get; set; }
		public string CssClass { get; set; }
		public string Href { get; set; }
		public string InnerHtml { get; set; }
		public bool IsExternal { get; set; }
		public bool IsActive { get; set; }
		public bool IsSubActive { get; set; }
		public bool IsDisabled { get; set; }


		/* functions */


		public TagBuilderExt GetTag(
			string innerHtml = null)
		{
			TagBuilderExt tag1;
			if (IsDisabled || string.IsNullOrEmpty(Href))
			{
				tag1 = new("span", TagRenderMode.Normal);
				tag1.AddCssClass("link-disabled disabled opacity-75");
				tag1.MergeAttribute("tabindex", "-1");
				tag1.MergeAttribute("aria-disabled", "true");
			}
			else
			{
				tag1 = new("a", TagRenderMode.Normal);
				tag1.MergeAttribute("href", SuppValues.Default(Href, "/"));
				if (IsActive)
				{
					tag1.AddCssClass("active");
					tag1.MergeAttribute("aria-current", "page");
				}
				if (IsExternal)
				{
					tag1.AddCssClass("link-external");
					tag1.MergeAttribute("target", "_blank");
				}
			}
			if (!string.IsNullOrEmpty(Id))
				tag1.MergeAttribute("id", Id);
			if (!string.IsNullOrEmpty(CssClass))
				tag1.AddCssClass(CssClass);
			tag1.InnerHtml.AppendHtml(innerHtml ?? InnerHtml);
			return tag1;
		}

	}

}
