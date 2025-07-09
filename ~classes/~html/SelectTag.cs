using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Ans.Net8.Web
{

	public class SelectTag
		: TagBuilderExt
	{

		/* ctor */


		public SelectTag(
			string name,
			string[] value,
			RegistryList registry,
			bool isMultiple)
			: base("select", TagRenderMode.Normal)
		{
			Name = name;
			Value = value;
			Registry = registry;
			IsMultiple = isMultiple;
			MergeAttribute("id", Name);
			MergeAttribute("name", Name);
			if (IsMultiple)
				MergeAttribute("multiple", "multiple");
			InnerHtml.AppendLine("");
			if (Registry?.HasItems ?? false)
				InnerHtml.AppendHtml(GetSelectOptions());
			else
				InnerHtml.AppendHtml($"<option>{Common.Resources.Common.Text_EmptyItem}</option>");
		}


		/* readonly properties */


		public string Name { get; }
		public string[] Value { get; }
		public RegistryList Registry { get; }
		public bool IsMultiple { get; }


		/* functions */


		public string GetSelectOptions()
		{
			if (Registry?.HasItems ?? false)
			{
				var sb1 = new StringBuilder();
				var items1 = IsMultiple
					? Registry.Items // .OrderBy(x => x.Value)
					: Registry.Items;
				bool f1 = false;
				foreach (var item1 in items1)
				{
					if (item1.IsLabel)
					{
						if (f1)
							sb1.AppendLine("</optgroup>");
						sb1.AppendLine($"<optgroup label=\"{item1.Value}\">");
						f1 = true;
					}
					else
					{
						var option1 = new OptionTag(
							item1.Key,
							item1.Value
								.Replace("&nbsp;", " ")
								.Replace('\u00a0', ' '),
							IsMultiple ? 0 : item1.Level,
							Value?.Contains(item1.Key) ?? false);
						sb1.AppendLine(option1.ToString());
					}
				}
				if (f1)
					sb1.AppendLine("</optgroup>");
				return sb1.ToString();
			}
			return null;
		}

	}

}
