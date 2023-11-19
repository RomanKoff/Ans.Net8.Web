using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class PageContext
		: _Context_Proto
	{

		/* ctor */


		public PageContext(
			ICurrentContext current)
			: base(current)
		{
		}


		/* properties */


		public override string FullTitle
		{
			get => CustomTitle
				?? _fullTitle
				?? Title;
			set => _fullTitle = value;
		}


		public string CustomTitle { get; set; }
		public string CustomBrowserTitle { get; set; }
		public MapPagesItem PageItem { get; set; }


		public string ResPath
		{
			get => _resPath
				?? _current.Request.PageResPath;
			set => _resPath = value;
		}
		private string _resPath;


		public string ContainerCss
		{
			get => _containerCss
				?? _current.Node.ContainerCss;
			set => _containerCss = value;
		}
		private string _containerCss;


		public string AddonCss { get; set; }


		public bool HideTitle { get; set; }
		public bool HideParentInTitle { get; set; }
		public bool HideBreadcrumbs { get; set; }


		public bool AllowTitle
			=> !HideTitle && !string.IsNullOrEmpty(FullTitle);


		/* readonly properties */


		public string ParentsTitle
			=> _current.Page.Breadcrumbs
				.SkipLast(1)
				.MakeFromCollection(
					x => x.InnerHtml, null, null, ". ");
		//private string _parentsTitle;


		public string ComplexTitle
			=> string.IsNullOrEmpty(ParentsTitle)
				? FullTitle
				: $"{ParentsTitle}. {FullTitle}";
		//private string _complexTitle;


		public string Css
			=> _css
				??= string.Join(" ", ContainerCss, AddonCss);
		private string _css;


		/* override */


		public override string ResUrl
		{
			get => _resUrl
				?? $"{_current.Node.ResUrl}/{ResPath}";
			set => _resUrl = value;
		}


		public string MainCss
		{
			get => _mainCss
				?? _current.Node.DefaultMainCss;
			set => _mainCss = value;
		}
		private string _mainCss;


		public string MainStyle
		{
			get => _mainStyle
				?? _current.Node.DefaultMainStyle;
			set => _mainStyle = value;
		}
		private string _mainStyle;


		public override LinkBuilder GetItemLink()
		{
			return (PageItem != null)
				? PageItem.Link
				: (Title != null)
					? new LinkBuilder { InnerHtml = Title, IsDisabled = true }
					: null;
		}

	}

}
