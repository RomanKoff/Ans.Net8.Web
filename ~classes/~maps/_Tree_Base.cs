namespace Ans.Net8.Web
{

	public interface I_Tree_Base<T>
		where T : _TreeItem_Base<T>
	{
		IEnumerable<T> AllItems { get; }
		IEnumerable<T> TopItems { get; }
		bool HasItems { get; }
	}



	public class _Tree_Base<T>
		: I_Tree_Base<T>
		where T : _TreeItem_Base<T>
	{

		internal readonly List<T> _allItems = new();


		/* readonly properties */


		public IEnumerable<T> AllItems
			=> _allItems;


		public IEnumerable<T> TopItems
			=> _topItems ??= _allItems.Where(x => !x.HasMasters);
		private IEnumerable<T> _topItems;


		public bool HasItems
			=> _allItems?.Any() ?? false;

	}

}
