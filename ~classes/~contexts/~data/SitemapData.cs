using Ans.Net8.Web.Services;

namespace Ans.Net8.Web
{

	public class SitemapData
		: _ContextModule_Base
	{

		private readonly IMapNodesProvider _nodesProvider;
		private readonly IMapPagesProvider _pagesProvider;


		/* ctor */


		public SitemapData(
			ICurrentContext current,
			IMapNodesProvider nodesProvider,
			IMapPagesProvider pagesProvider)
			: base(current)
		{
			_nodesProvider = nodesProvider;
			_pagesProvider = pagesProvider;
		}


		/* functions */


		public MapNodes GetMapNodes()
		{
			return _nodesProvider.GetMapNodes(_current.Host.VirtualPath);
		}


		public MapPages GetMapPages(
			string node)
		{
			return _pagesProvider.GetMapPages(node, _current.Host.VirtualPath);
		}


		/* methods */


		public void MapNodesReset()
		{
			foreach (var node in GetMapNodes().AllItems)
				MapPagesReset(node.Name);
			_nodesProvider.Reset();
		}


		public void MapPagesReset(string node)
		{
			_pagesProvider.Reset(node);
		}

	}

}
