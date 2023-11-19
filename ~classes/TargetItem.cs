namespace Ans.Net8.Web
{

	public class TargetItem
	{

		/* ctor */

		// TODO: Disabled

		public TargetItem(
			string target,
			string innerHtml,
			string hostVirtualPath)
		{
			Link = new LinkBuilder { InnerHtml = innerHtml };
			if (target == null)
				Link.IsDisabled = true;
			else if (string.IsNullOrEmpty(target))
				Link.Href = hostVirtualPath;
			else if (target[0] == '/')
				Link.Href = $"{hostVirtualPath}{target[1..]}";
			else if (Common._Consts.G_REGEX_IPATH().IsMatch(target))
				Link.Href = $"{hostVirtualPath}{target}";
			else
			{
				Link.Href = target;
				Link.IsExternal = true;
			}
		}


		/* readonly properties */


		public LinkBuilder Link { get; private set; }

	}

}
