namespace Ans.Net8.Web
{

	public class TagOptionModel
	{
		public string Value { get; set; }
		public string Inner { get; set; }
		public bool IsSelected { get; set; }

		public int Level { get; set; }
	}



	public class TagSelectModel
	{
		public TagOptionModel Selected { get; set; }
		public IEnumerable<TagOptionModel> Options { get; set; }
	}

}