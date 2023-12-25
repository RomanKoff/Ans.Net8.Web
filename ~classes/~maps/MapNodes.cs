namespace Ans.Net8.Web
{

	public class MapNodes(
		IEnumerable<MapNodesItem> source,
		string hostVirtualPath)
		: _Tree_Base<MapNodesItem>(source)
	{

		private readonly string _hostVirtualPath = hostVirtualPath;


		/* overrides methods */


		public override void PrepareItemBefore(
			MapNodesItem item)
		{
			item.InitLink(_hostVirtualPath);
		}


		/* functions */


		public MapNodesItem GetNode(
			string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;
			return AllItems.FirstOrDefault(
				x => x.Name == name);
		}

	}

}
