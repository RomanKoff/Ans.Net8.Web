namespace Ans.Net8.Web
{

	public class MapNodesItem
		: _TreeItem_Base<MapNodesItem>
	{

		/* ctor */


		public MapNodesItem(
			string target,
			string face,
			bool isHideen,
			string tags)
			: base(face ?? target, isHideen, tags)
		{
			_parseTarget(target);
		}


		/* properties */


		public string Path { get; set; }


		/* methods */


		public void InitLink(
			string hostVirtualPath)
		{
			Link = new LinkBuilder { InnerHtml = ShortTitle };
			switch (Type)
			{
				case MapItemTypeEnum.Catalog:
					Link.IsDisabled = true;
					break;
				case MapItemTypeEnum.Item:
					Link.Href = $"{hostVirtualPath}{Name}";
					break;
				case MapItemTypeEnum.InternalPath:
					Link.Href = $"{hostVirtualPath}{Target}";
					break;
				default:
					Link.Href = Target;
					Link.IsExternal = true;
					break;
			}
		}


		/* privates */


		private void _parseTarget(
			string target)
		{
			if (string.IsNullOrEmpty(target))
			{
				Type = MapItemTypeEnum.Catalog;
			}
			else if (target[0] == '/')
			{
				Type = MapItemTypeEnum.InternalPath;
				Target = target[1..];
			}
			else if (Common._Consts.G_REGEX_NAME().IsMatch(target))
			{
				Type = MapItemTypeEnum.Item;
				Name = target;
			}
			else
			{
				Type = MapItemTypeEnum.ExternalLink;
				Target = target;
			}
		}

	}

}
