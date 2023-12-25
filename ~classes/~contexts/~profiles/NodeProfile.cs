namespace Ans.Net8.Web
{

	public class NodeProfile(
		ICurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* overrides properties */


		public override string ContainerCss
		{
			get => _containerCss ?? _current.Site.ContainerCss;
			set => _containerCss = value;
		}

		public override string ResUrl
		{
			get => _resUrl ?? $"{_current.Site.ResUrl}/{ResPath}";
			set => _resUrl = value;
		}


		/* properties */


		public MapNodesItem NodeItem { get; set; }
		public string Name { get; set; }
		public NodeCustomizer Custom { get; set; } = new();

		private string _mainCss;
		public string DefaultMainCss
		{
			get => _mainCss ?? _current.Site.DefaultMainCss;
			set => _mainCss = value;
		}

		private string _mainStyle;
		public string DefaultMainStyle
		{
			get => _mainStyle ?? _current.Site.DefaultMainStyle;
			set => _mainStyle = value;
		}

		private string _resPath;
		public string ResPath
		{
			get => _resPath ?? _current.Request.NodeName;
			set => _resPath = value;
		}


		/* readonly properties */


		public MapPages MapPages
			=> _current.Maps.GetPages(Name);


		/* overrides functions */


		public override LinkBuilder GetItemLink()
		{
			return (NodeItem != null)
				? NodeItem.Link
				: (Title != null)
					? new LinkBuilder { InnerHtml = Title, IsDisabled = true }
					: null;
		}


		/* methods */


		public void InsertMasterNode(
			string title,
			string name)
		{
			InsertParent(new LinkBuilder
			{
				InnerHtml = title,
				Href = $"{_current.Host.VirtualPath}{name}"
			});
		}

	}

}
