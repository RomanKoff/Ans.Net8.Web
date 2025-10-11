namespace Ans.Net8.Web
{

	public class MediaModel
	{
		public string Url { get; set; }
		public string Label { get; set; }
		public MediaDefModel Def { get; set; }

		public bool HasLabel
			=> !string.IsNullOrEmpty(Label);
	}



	public class MediaDefModel
	{
		public MediaDefModel(
			string def)
		{
			if (def.StartsWith("IMG:"))
			{
				Inner = def[4..];
				Type = MediaDefTypeEnum.Image;
				return;
			}
			if (def.StartsWith("TEXT:"))
			{
				Inner = def[5..];
				Type = MediaDefTypeEnum.Text;
				return;
			}
			Inner = def;
			Type = MediaDefTypeEnum.Icon;
		}

		public string Inner { get; }
		public MediaDefTypeEnum Type { get; }
	}



	public enum MediaDefTypeEnum
	{
		Icon = 0,
		Image = 1,
		Text = 2,
	}

}
