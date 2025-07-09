namespace Ans.Net8.Web
{

	public class MapNodes
		: _Map_Base<MapNodesItem>
	{

		/* ctor */


		public MapNodes(
			IEnumerable<MapNodesItem> items,
			string virtualPath)
			: base(items)
		{
			_scan(items, null, virtualPath);
		}


		/* functions */


		public override MapNodesItem GetItem(
			string find)
		{
			return (MapNodesItem)AllItems
				.FirstOrDefault(x => x.Target == find);
		}


		/* privates */


		private static void _scan(
			IEnumerable<IMapItem> items,
			MapNodesItem master,
			string virtualPath)
		{
			foreach (var item1 in items)
			{
				var item2 = (MapNodesItem)item1;
				item2.ParseTarget(master, virtualPath);
				if (item1.HasSlaves)
					_scan(item1.Slaves, item2, virtualPath);
			}
		}

	}

}
