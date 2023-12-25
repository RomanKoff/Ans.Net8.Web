using System.Xml.Linq;

namespace Ans.Net8.Web
{

	public interface ITree<T>
		where T : ITreeItem<T>
	{
		IEnumerable<T> TopItems { get; }
		IEnumerable<T> AllItems { get; }
		T FirstItem { get; }
	}



	public class _Tree_Base<T>
		: ITree<T>
		where T : ITreeItem<T>
	{

		private readonly List<T> _allItems = [];


		public virtual void PrepareItemBefore(T item) { }
		public virtual void PrepareItemAfter(T item) { }


		/* ctor */


		public _Tree_Base(
			IEnumerable<T> source)
		{
			_scan(source, default);
			TopItems = source;
			AllItems = _allItems.AsEnumerable();
			FirstItem = _allItems.First();
		}


		/* readonly properties */


		public IEnumerable<T> TopItems { get; }
		public IEnumerable<T> AllItems { get; }
		public T FirstItem { get; }


		/* functions */


		public T GetItem(
			Func<T, bool> find)
			=> AllItems.FirstOrDefault(find);


		/* privates */


		private void _scan(
			IEnumerable<T> items,
			T master)
		{
			if (!(items?.Count() > 0))
				return;
			foreach (var item1 in items)
			{
				_allItems.Add(item1);
				if (master != null)
				{
					var a1 = new List<T>();
					if (master.HasMasters)
						a1.AddRange(master.Masters);
					a1.Add(master);
					item1.Masters = a1.AsEnumerable();
				}
				PrepareItemBefore(item1);
				_scan(item1.Slaves, item1);
				PrepareItemAfter(item1);
			}
		}

	}

}
