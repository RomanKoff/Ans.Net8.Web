namespace Ans.Net8.Web
{

	public class SiteProfile(
		ICurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* overrides properties */


		public override string ContainerCss
		{
			get => _containerCss ?? _current.Options.DefaultCssContainer ?? "container";
			set => _containerCss = value;
		}

		public override string ResUrl
		{
			get => _resUrl ?? $"{_current.Host.VirtualPath}content";
			set => _resUrl = value;
		}


		/* properties */


		public string HomeUrl { get; set; }
		public string CustomFaviconType { get; set; }
		public string CustomFaviconHref { get; set; }
		public string ManifestJsonHref { get; set; }
		public string DefaultMainCss { get; set; }
		public string DefaultMainStyle { get; set; }
		public string AddonStylesheetHref { get; set; }
		public string AddonStylesheetCrossorigin { get; set; }


		/* readonly properties */


		public MapNodes MapNodes
			=> _current.Maps.GetNodes();


		/* overrides functions */


		public override LinkBuilder GetItemLink()
		{
			if (Title == null)
				return null;
			return new LinkBuilder
			{
				InnerHtml = Title,
				Href = HomeUrl ?? _current.Host.VirtualPath
			};
		}


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

	}

}
