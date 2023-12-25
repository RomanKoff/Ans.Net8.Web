using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public class PageProfile(
		ICurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* overrides properties */


		public override string ContainerCss
		{
			get => _containerCss ?? _current.Node.ContainerCss;
			set => _containerCss = value;
		}

		public override string ResUrl
		{
			get => _resUrl ?? $"{_current.Node.ResUrl}/{ResPath}";
			set => _resUrl = value;
		}

		public override string FullTitle
		{
			get => CustomTitle
				?? _fullTitle
				?? Title;
			set => _fullTitle = value;
		}


		/* properties */


		public MapPagesItem PageItem { get; set; }
		public string AddonCss { get; set; }
		public string CustomTitle { get; set; }
		public string CustomBrowserTitle { get; set; }

		private string _mainCss;
		public string MainCss
		{
			get => _mainCss ?? _current.Node.DefaultMainCss;
			set => _mainCss = value;
		}

		private string _mainStyle;
		public string MainStyle
		{
			get => _mainStyle ?? _current.Node.DefaultMainStyle;
			set => _mainStyle = value;
		}

		private string _resPath;
		public string ResPath
		{
			get => _resPath ?? _current.Request.PageResPath;
			set => _resPath = value;
		}

		public bool HideTitle { get; set; }
		public bool HideParentInTitle { get; set; }
		public bool HideInBreadcrumbs { get; set; }
		public bool HideBreadcrumbs { get; set; }


		/* readonly properties */


		public string ParentsTitle
			=> _current.Page.Breadcrumbs
				.SkipLast(1)
				.MakeFromCollection(x => x.InnerHtml, null, null, ". ");

		public string ComplexTitle
			=> string.IsNullOrEmpty(ParentsTitle)
				? FullTitle
				: $"{ParentsTitle}. {FullTitle}";

		private string _calcCss;
		public string CalcCss
			=> _calcCss ??= string.Join(" ", ContainerCss, AddonCss);

		public bool AllowTitle
			=> !HideTitle && !string.IsNullOrEmpty(FullTitle);


		/* overrides functions */


		public override LinkBuilder GetItemLink()
		{
			return (PageItem != null)
				? PageItem.Link
				: (!HideInBreadcrumbs && Title != null)
					? new LinkBuilder { InnerHtml = Title, IsDisabled = true }
					: null;
		}


		/* functions */


		public HtmlString SlaveLink(
			string name,
			string title)
		{
			var link1 = new LinkBuilder(
				$"{_current.Request.Url}/{name}", title);
			return link1.GetTag().ToHtml();
		}


		public HtmlString SlaveLink(
			string name)
		{
			return SlaveLink(name, name);
		}

	}

}
