namespace Ans.Net8.Web
{

	public class SiteProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* properties */


		private string _containerClasses;
		public override string ContainerClasses
		{
			get => _containerClasses ??= _Current.Options.DefaultCssContainer ?? "container";
			set => _containerClasses = value;
		}


		private string _resUrl;
		public override string ResUrl
		{
			get => _resUrl ??= $"{Url}/content";
			set => _resUrl = value;
		}


		/* readonly properties */


		public override string Url
			=> _Current.Host.ApplicationUrl;


		private MapNodes _mapNodes;
		public MapNodes MapNodes
			=> _mapNodes ??= _getPrepMapNodes();


		public bool HasNodes
			=> MapNodes?.HasItems ?? false;


		/* functions */


		public LinkBuilder GetNodeLink(
			MapNodesItem node)
		{
			var link1 = new LinkBuilder(null, node.ShortTitle);
			switch (node.Type)
			{
				case MapItemTypeEnum.Group:
					link1.IsDisabled = true;
					break;
				case MapItemTypeEnum.InternalPath:
					link1.Href = $"{_Current.Host.VirtualPath}{node.Target}";
					break;
				case MapItemTypeEnum.ExternalUrl:
					link1.Href = node.Target;
					link1.IsExternal = true;
					break;
				default: // Node
					link1.Href = $"{_Current.Host.VirtualPath}/{node.Target}";
					break;
			}
			return link1;
		}


		/* privates */


		private MapNodes _getPrepMapNodes()
		{
			var map1 = _Current.Maps.GetMapNodes();
			_prepMapNodes(map1.TopItems);
			return map1;
		}


		private void _prepMapNodes(
			IEnumerable<IMapItem> items)
		{
			if (items?.Count() > 0)
				foreach (var item1 in items)
				{
					var item2 = (MapNodesItem)item1;
					item2.Link = GetNodeLink(item2);
					_prepMapNodes(item2.Slaves);
				}
		}

	}

}
