namespace Ans.Net8.Web
{

	public enum MediaModeEnum
	{
		None = 0,
		Known = 1,
		Other = 2,
	}



	public class MediaModel
	{
		public string Url { get; set; }
		public string Title { get; set; }
		public string Icon { get; set; }
		public MediaModeEnum Mode { get; set; }
	}

}
