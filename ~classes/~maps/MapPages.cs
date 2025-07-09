namespace Ans.Net8.Web
{

	public class MapPages
		: _Map_Base<MapPagesItem>
	{

		/* ctor */


		public MapPages(
			IEnumerable<MapPagesItem> items,
			string virtualPath,
			string node)
			: base(items)
		{
			if (items?.Count() > 0)
				_scan(items, null, virtualPath, node);
		}


		/* functions */


		public override MapPagesItem GetItem(
			string find)
		{
			return (MapPagesItem)AllItems
				.FirstOrDefault(x => x.Link.Href == find);
		}


		/* privates */


		private static void _scan(
			IEnumerable<IMapItem> items,
			MapPagesItem master,
			string virtualPath,
			string node)
		{
			foreach (var item1 in items)
			{
				var item2 = (MapPagesItem)item1;
				item2.ParseTarget(master, virtualPath, node);
				if (item1.HasSlaves)
					_scan(item1.Slaves, item2, virtualPath, node);
			}
		}

	}

}
