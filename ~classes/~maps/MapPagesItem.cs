using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class MapPagesItem
		: _MapItem_Base,
		IMapItem
	{

		/* ctors */


		public MapPagesItem(
			string target,
			string face,
			bool hasStart = false,
			bool isHidden = false,
			string tags = null,
			string properties = null,
			string remark = null)
		{
			Target = target;
			HasStart = hasStart;
			IsHidden = isHidden;
			Tags = tags?.Split(Common._Consts.SEPS_ARRAY);
			Properties = properties?.Split(';').ToStringDictionary();
			Remark = remark;
			SetFace(face ?? target);
			Link = new(null, ShortTitle);
			//{
			//	InnerHtml = ShortTitleHtml.ToString()
			//};
		}


		public MapPagesItem(
			string target,
			string face,
			params MapPagesItem[] pages)
			: this(target, face)
		{
			if (pages?.Length > 0)
				Slaves = [.. pages];
		}


		/* readonly properties */


		public bool HasStart { get; }


		public MapPagesItem Parent
			=> HasMasters ? (MapPagesItem)Masters.Last() : null;


		/* methods */


		public void ParseTarget(
			MapPagesItem master,
			string virtualPath,
			string path)
		{
			if (master != null)
				Masters = master.HasMasters
					? master.Masters?.Append(master)
					: [master];

			var path1 = HasMasters
				? Parent?.Link.Href
				: $"{virtualPath}{path?.Make("/{0}")}";

			if (string.IsNullOrEmpty(Target))
			{
				// start page
				Id = "/start";
				Type = MapItemTypeEnum.Item;
				Link.Href = path1;
				return;
			}

			if (Common._Consts.G_REGEX_NAME().IsMatch(Target))
			{
				// page
				Id = $"{path1}/{Target}";
				Type = MapItemTypeEnum.Item;
				Link.Href = Id;
				return;
			}

			if (Target[0] == '/')
			{
				// internal
				Type = MapItemTypeEnum.InternalPath;
				Link.Href = $"{virtualPath}{Target}";
				return;
			}

			// external
			Type = MapItemTypeEnum.ExternalUrl;
			Link.Href = Target;
			Link.IsExternal = true;
		}

	}

}
