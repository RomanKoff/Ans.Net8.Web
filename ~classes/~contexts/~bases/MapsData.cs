using Ans.Net8.Web.Services;

namespace Ans.Net8.Web
{

	public class MapsData(
		ICurrentContext current,
		IMapNodesProvider mapNodesProvider,
		IMapPagesProvider mapPagesProvider)
		: _Current_Base(current)
	{

		private readonly IMapNodesProvider _mapNodesProvider = mapNodesProvider;
		private readonly IMapPagesProvider _mapPagesProvider = mapPagesProvider;


		/* properties */


		public MapNodes GetNodes()
			=> _mapNodesProvider.GetMap(_current.Host.VirtualPath);

		public MapPages GetPages(
			string node)
			=> _mapPagesProvider.GetMap(node, _current.Host.VirtualPath);


		/* methods */


		public void ResetNodes()
		{
			foreach (var node1 in GetNodes().AllItems)
				ResetPages(node1.Name);
			_mapNodesProvider.Reset();
		}


		public void ResetPages(
			string node)
		{
			_mapPagesProvider.Reset(node);
		}

	}

}
