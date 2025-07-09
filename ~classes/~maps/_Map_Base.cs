namespace Ans.Net8.Web
{

	public abstract class _Map_Base<T>
		where T : IMapItem
	{

		private readonly List<IMapItem> _all = [];


		/* abstracts */


		public abstract T GetItem(string find);


		/* ctor */


		public _Map_Base(
			IEnumerable<IMapItem> items)
		{
			_scan(items);
			AllItems = _all.AsEnumerable();
			TopItems = items;
		}


		/* readonly properties */


		public IEnumerable<IMapItem> AllItems { get; }
		public IEnumerable<IMapItem> TopItems { get; }


		public bool HasItems
			=> TopItems?.Count() > 0;


		/* methods */


		public void ClearActives()
		{
			foreach (var item1 in AllItems)
				item1.ClearActive();
		}


		/* privates */


		private void _scan(
			IEnumerable<IMapItem> items)
		{
			if (items?.Count() > 0)
				foreach (var item1 in items)
				{
					_all.Add(item1);
					_scan(item1.Slaves);
				}
		}

	}

}
