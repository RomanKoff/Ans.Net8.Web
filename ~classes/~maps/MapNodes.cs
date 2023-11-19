namespace Ans.Net8.Web
{

	public class MapNodes
		: _Tree_Base<MapNodesItem>
	{

		private readonly string _hostVirtualPath;


		/* ctor */


		public MapNodes(
			IEnumerable<MapNodesItem> nodes,
			string hostVirtualPath)
		{
			_hostVirtualPath = hostVirtualPath;
			_ = _prepTree(nodes, null);
			FirstItem = _allItems!.FirstOrDefault();
		}


		/* readonly properties */


		public MapNodesItem FirstItem { get; private set; }


		/* functions */


		public MapNodesItem GetNode(
			string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;
			return _allItems.FirstOrDefault(
				x => x.Name == name);
		}


		/* privates */


		private IEnumerable<MapNodesItem> _prepTree(
			IEnumerable<MapNodesItem> items,
			MapNodesItem master)
		{
			if (!items?.Any() ?? true)
				return null;
			var items1 = new List<MapNodesItem>();
			foreach (var item1 in items)
			{
				_allItems.Add(item1);
				if (master != null)
				{
					var a1 = new List<MapNodesItem>();
					if (master.HasMasters)
						a1.AddRange(master.Masters);
					a1.Add(master);
					item1.Masters = a1;
				}
				item1.InitLink(_hostVirtualPath);
				item1.Slaves = _prepTree(item1.Slaves, item1);
				items1.Add(item1);
			}
			return items1;
		}

	}

}
