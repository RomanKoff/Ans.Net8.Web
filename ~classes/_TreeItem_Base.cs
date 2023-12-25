namespace Ans.Net8.Web
{

	public interface ITreeItem<T>
	{
		bool HasMasters { get; }
		bool HasSlaves { get; }
		IEnumerable<T> Masters { get; set; }
		IEnumerable<T> Slaves { get; set; }
	}



	public class _TreeItem_Base<T>
		: ITreeItem<T>
		where T : ITreeItem<T>
	{
		public IEnumerable<T> Masters { get; set; }
		public IEnumerable<T> Slaves { get; set; }

		public bool HasMasters
			=> Masters?.Count() > 0;

		public bool HasSlaves
			=> Slaves?.Count() > 0;
	}

}
