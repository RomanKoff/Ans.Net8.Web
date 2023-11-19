using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class LinkBuilder
	{

		/* ctors */


		public LinkBuilder()
		{
		}


		public LinkBuilder(
			string innerHtml)
			: this()
		{
			InnerHtml = innerHtml;
			IsDisabled = true;
		}


		public LinkBuilder(
			string href,
			string innerHtml)
			: this()
		{
			Href = href;
			InnerHtml = innerHtml;
			IsDisabled = string.IsNullOrEmpty(href);
		}


		/* properties */


		public string Id { get; set; }
		public string CssClass { get; set; }
		public string Href { get; set; }
		public string InnerHtml { get; set; }
		public bool IsExternal { get; set; }
		public bool IsActive { get; set; }
		public bool IsDisabled { get; set; }


		/* functions */


		public TagBuilder GetTag()
		{
			TagBuilder tag1;
			if (IsDisabled)
			{
				tag1 = new TagBuilder("span")
				{
					TagRenderMode = TagRenderMode.Normal
				};
				tag1.AddCssClass("link-disabled disabled opacity-75");
				tag1.MergeAttribute("tabindex", "-1");
				tag1.MergeAttribute("aria-disabled", "true");
			}
			else
			{
				tag1 = new TagBuilder("a")
				{
					TagRenderMode = TagRenderMode.Normal
				};
				tag1.MergeAttribute("href", Href);
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
			tag1.InnerHtml.AppendHtml(InnerHtml);
			return tag1;
		}

	}

}
