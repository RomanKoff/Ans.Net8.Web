using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class MapPages
		: _Tree_Base<MapPagesItem>
	{

		private readonly string _hostNodePath;
		private readonly string _hostVirtualPath;


		/* ctor */


		public MapPages(
			IEnumerable<MapPagesItem> pages,
			string node,
			string hostVirtualPath)
		{
			_hostNodePath = $"{hostVirtualPath}{node.Make("{0}/")}";
			_hostVirtualPath = hostVirtualPath;
			_ = _prepTree(pages, null);
		}


		/* functions */


		public MapPagesItem GetPage(
			string path)
		{
			foreach (var item1 in _allItems)
			{
				item1.Link.IsActive = false;
				item1.IsSubActive = false;
			}
			var pages1 = _allItems.Where(x => x.Path == path);
			if (!pages1.Any())
				return null;
			var page1 = (pages1.Count() == 1)
				? pages1.First()
				: pages1.LastOrDefault(x => !x.IsHidden);
			if (page1 != null)
			{
				page1.Link.IsActive = true;
				page1.MakeSupActive();
			}
			return page1;
		}


		/* privates */


		private IEnumerable<MapPagesItem> _prepTree(
			IEnumerable<MapPagesItem> items,
			MapPagesItem master)
		{
			if (!items?.Any() ?? true)
				return null;
			var items1 = new List<MapPagesItem>();
			foreach (var item1 in items)
			{
				_allItems.Add(item1);
				if (master != null)
				{
					var a1 = new List<MapPagesItem>();
					if (master.HasMasters)
						a1.AddRange(master.Masters);
					a1.Add(master);
					item1.Masters = a1;
				}
				item1.InitLink(_hostNodePath, _hostVirtualPath);
				item1.Slaves = _prepTree(item1.Slaves, item1);
				items1.Add(item1);
			}
			return items1;
		}

	}

}
