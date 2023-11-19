namespace Ans.Net8.Web
{

	public class SiteContext
		: _Context_Proto
	{

		/* ctor */


		public SiteContext(
			ICurrentContext current)
			: base(current)
		{
		}


		/* properties */


		public string HomeUrl { get; set; }


		public string ContainerCss
		{
			get => _containerCss
				?? _current.Options.DefaultCssContainer
				?? "container";
			set => _containerCss = value;
		}
		private string _containerCss;


		public string DefaultMainCss { get; set; }
		public string DefaultMainStyle { get; set; }
		public string BrowserIconType { get; set; }
		public string BrowserIconHref { get; set; }
		public string ManifestJsonHref { get; set; }
		public string AddonStylesheetHref { get; set; }
		public string AddonStylesheetCrossorigin { get; set; }


		/* readonly properties */


		public MapNodes MapNodes
			=> _current.Sitemap.GetMapNodes();


		/* methods */


		public void InsertMasterSite(
			string title,
			string url,
			bool isExternal)
		{
			InsertParent(new LinkBuilder
			{
				InnerHtml = title,
				Href = url,
				IsExternal = isExternal
			});
		}


		/* overrides */


		public override string ResUrl
		{
			get => _resUrl
				?? $"{_current.Host.VirtualPath}content";
			set => _resUrl = value;
		}


		public override LinkBuilder GetItemLink()
		{
			if (Title == null)
				return null;
			return new LinkBuilder
			{
				InnerHtml = Title,
				Href = HomeUrl
					?? _current.Host.VirtualPath
			};
		}

	}

}
