namespace Ans.Net8.Web
{

	public class MapNodesItem
		: _TreeItem_Base<MapNodesItem>
	{

		/* ctor */


		public MapNodesItem(
			string id,
			string target,
			string face,
			bool isHidden,
			string tags)
		{
			_parseFace(face);
			_parseTarget(target);
			Id = id;
			IsHidden = isHidden;
			Tags = tags?.Split(',', ';');
		}


		/* readonly properties */


		public MapNodesItemType Type { get; private set; }
		public LinkBuilder Link { get; private set; }
		public string Name { get; private set; }
		public string Target { get; private set; }
		public string Title { get; private set; }

		public string ShortTitle
			=> _shortTitle ?? Title;
		private string _shortTitle;

		public bool HasShortTitle
			=> _shortTitle != null;

		public string Id { get; }
		public bool IsHidden { get; }
		public string[] Tags { get; }


		/* methods */


		public void InitLink(
			string hostVirtualPath)
		{
			Link = new LinkBuilder { InnerHtml = ShortTitle };
			switch (Type)
			{
				case MapNodesItemType.Group:
					Link.IsDisabled = true;
					break;
				case MapNodesItemType.Node:
					Link.Href = $"{hostVirtualPath}{Name}";
					break;
				case MapNodesItemType.InternalPath:
					Link.Href = $"{hostVirtualPath}{Target}";
					break;
				default:
					Link.Href = Target;
					Link.IsExternal = true;
					break;
			}
		}


		/* functions */


		public LinkBuilder GetNavLink()
		{
			return (HasSlaves)
				? new LinkBuilder
				{
					CssClass = Link.CssClass,
					Href = Link.Href,
					Id = Link.Id,
					InnerHtml = Link.InnerHtml,
					IsActive = Link.IsActive,
					IsExternal = Link.IsExternal,
					IsDisabled = false
				}
				: Link;
		}


		/* privates */


		private void _parseFace(
			string face)
		{
			if (!string.IsNullOrEmpty(face))
			{
				int i1 = face.IndexOf('|');
				if (i1 == -1)
					Title = face;
				else
				{
					_shortTitle = face[..i1];
					Title = string.Format(face[(i1 + 1)..], _shortTitle);
				}
			}
		}


		private void _parseTarget(
			string target)
		{
			if (string.IsNullOrEmpty(target))
			{
				Type = MapNodesItemType.Group;
			}
			else if (target[0] == '/')
			{
				Type = MapNodesItemType.InternalPath;
				Target = target[1..];
			}
			else if (Common._Consts.G_REGEX_NAME().IsMatch(target))
			{
				Type = MapNodesItemType.Node;
				Name = target;
			}
			else
			{
				Type = MapNodesItemType.ExternalLink;
				Target = target;
			}
		}

	}



	public enum MapNodesItemType
	{
		Group,
		Node,
		InternalPath,
		ExternalLink
	}

}
