using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class NodeProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* properties */


		public MapNodesItem NodeItem { get; set; }


		private string _containerClasses;
		public override string ContainerClasses
		{
			get => _containerClasses ?? _Current.Site.ContainerClasses;
			set => _containerClasses = value;
		}


		public override string Title
		{
			get => base.Title ?? NodeItem?.Title;
			set => base.Title = value;
		}


		public override string ShortTitle
		{
			get => IsShortTitleUnique
				? base.ShortTitle
				: NodeItem?.ShortTitle;
			set => base.ShortTitle = value;
		}


		private string _resPath;
		public string ResPath
		{
			get => _resPath ??= _Current.Request.NodeName;
			set
			{
				_resPath = value;
				_resUrl = null;
			}
		}


		private string _resUrl;
		public override string ResUrl
		{
			get => _resUrl ??= $"{_Current.Site.ResUrl}{ResPath.Make("/{0}")}";
			set => _resUrl = value;
		}


		/* readonly properties */


		public override string Url
			=> $"{_Current.Site.Url}{NodeItem?.Target?.Make("/{0}")}";


		private MapPages _mapPages;
		public MapPages MapPages
		{
			get => _mapPages ?? _Current.Maps.GetMapPages(
				NodeItem?.Target); //?? _Current.Request.NodeName);
			set => _mapPages = value;
		}


		public bool HasPages
			=> MapPages?.HasItems ?? false;


		public bool HasSlaves
			=> NodeItem?.HasSlaves ?? false;


		/* methods */


		public void SetPages(
			string path,
			params MapPagesItem[] pages)
		{
			MapPages = new(pages, _Current.Host.VirtualPath, path);
		}

	}

}
