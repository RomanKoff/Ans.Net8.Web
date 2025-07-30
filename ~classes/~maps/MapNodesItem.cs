using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class MapNodesItem
		: _MapItem_Base,
		IMapItem
	{

		/* ctors */


		public MapNodesItem(
			string target,
			string face,
			bool isHidden = false,
			string tags = null,
			string properties = null,
			string remark = null)
		{
			Target = target;
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


		/* readonly properties */


		public MapNodesItem Parent
			=> HasMasters ? (MapNodesItem)Masters.Last() : null;


		/* methods */


		public void ParseTarget(
			MapNodesItem master,
			string virtualPath)
		{
			if (master != null)
				Masters = master.HasMasters
					? master.Masters?.Append(master)
					: [master];

			if (Target == null)
			{
				// group
				Id = Target;
				Type = MapItemTypeEnum.Group;
				Link.IsDisabled = true;
				return;
			}

			if (Common._Consts.G_REGEX_NAME().IsMatch(Target))
			{
				// node
				Id = Target;
				Type = MapItemTypeEnum.Item;
				Link.Href = $"{virtualPath}/{Target}";
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
