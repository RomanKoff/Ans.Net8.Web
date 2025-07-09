using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public static partial class _e_TagBuilder
	{

		/* methods */


		public static void AddStyle(
			this TagBuilder tag,
			string value)
		{
			if (tag.Attributes.TryGetValue("style", out var currentValue))
				tag.Attributes["style"] = currentValue + value;
			else
				tag.Attributes["style"] = value;
		}

	}

}
