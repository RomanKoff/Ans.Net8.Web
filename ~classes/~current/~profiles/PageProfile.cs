using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class PageProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* properties */


		public MapPagesItem PageItem { get; set; }


		private string _containerClasses;
		public override string ContainerClasses
		{
			get => _containerClasses ?? _Current.Node.ContainerClasses;
			set => _containerClasses = value;
		}


		public override string Title
		{
			get => base.Title ?? PageItem?.Title;
			set => base.Title = value;
		}


		public override string ShortTitle
		{
			get => IsShortTitleUnique
				? base.ShortTitle
				: base.Title ?? PageItem?.ShortTitle;
			set => base.ShortTitle = value;
		}


		public string CustomBrowserTitle { get; set; }


		private string _resPath;
		public string ResPath
		{
			get => _resPath ??= _Current.Request.PagePath;
			set
			{
				_resPath = value;
				_resUrl = null;
			}
		}


		private string _resUrl;
		public override string ResUrl
		{
			get => _resUrl ??= $"{_Current.Node.ResUrl}{ResPath.Make("/{0}")}";
			set => _resUrl = value;
		}


		/* readonly properties */


		public override string Url
			=> $"{_Current.Node.Url}{_Current.Request.PagePath.Make("/{0}")}";


		private string _parentsTitles;
		public string ParentsTitles
			=> _parentsTitles ??= _getParentsTitles();


		public bool HasSlaves
			=> PageItem?.HasSlaves ?? false;


		/* methods */


		public void ImportTitleFromLastBreadcrumbs()
		{
			var item1 = ParentsLinks?.Count > 0
				? ParentsLinks.Last() : null;
			Title = item1?.InnerHtml;
		}


		/* privates */


		private string _getParentsTitles()
		{
			return ParentsLinks?.MakeFromCollection(
				x => x.InnerHtml, null, null, ". ")
				.GetTypografMin();
		}

	}

}
