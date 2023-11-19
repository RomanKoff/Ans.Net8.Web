namespace Ans.Net8.Web
{

	public class MapActions
		: _Tree_Base<MapActionsItem>
	{

		private readonly string _hostVirtualPath;


		/* ctor */


		public MapActions(
			IEnumerable<MapActionsItem> actions,
			string hostVirtualPath)
		{
			_hostVirtualPath = hostVirtualPath;
			_ = _prepTree(actions, null);
		}


		/* functions */


		public MapActionsItem GetAction(
			string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;
			return _allItems.FirstOrDefault(
				x => x.Name == name);
		}


		/* privates */


		private IEnumerable<MapActionsItem> _prepTree(
			IEnumerable<MapActionsItem> items,
			MapActionsItem master)
		{
			if (!items?.Any() ?? true)
				return null;
			var items1 = new List<MapActionsItem>();
			foreach (var item1 in items)
			{
				_allItems.Add(item1);
				if (master != null)
				{
					var a1 = new List<MapActionsItem>();
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
