using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class MapPages(
		IEnumerable<MapPagesItem> source,
		string node,
		string hostVirtualPath)
		: _Tree_Base<MapPagesItem>(source)
	{

		private readonly string _hostVirtualPath = hostVirtualPath;
		private readonly string _hostNodePath = $"{hostVirtualPath}{node.Make("{0}/")}";


		/* overrides methods */


		public override void PrepareItemBefore(
			MapPagesItem item)
		{
			var pathParent1 = item.Masters?.Last().Path;
			var hasPath1 = !string.IsNullOrEmpty(pathParent1);
			var hasPageName1 = !string.IsNullOrEmpty(item.Name);
			item.Path = hasPath1 && hasPageName1
				? $"{pathParent1}/{item.Name}" : hasPath1
					? pathParent1 : item.Name;
			item.InitLink(_hostNodePath, _hostVirtualPath);
		}


		/* functions */


		public MapPagesItem GetPage(
			string path)
		{
			//if (string.IsNullOrEmpty(path))
			//	return null;
			foreach (var item1 in AllItems)
			{
				item1.Link.IsActive = false;
				item1.IsSubActive = false;
			}
			var items1 = AllItems.Where(x => x.Path == path);
			var count1 = items1.Count();
			if (count1 == 0)
				return null;
			var page1 = count1 == 1
				? items1.First()
				: items1.LastOrDefault(x => !x.IsHidden); // для заглушек разделов
			if (page1 != null)
			{
				page1.Link.IsActive = true;
				page1.MakeSupActive();
			}
			return page1;
		}

	}

}
