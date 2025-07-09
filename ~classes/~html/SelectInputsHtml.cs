using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Ans.Net8.Web
{

	public class SelectInputsHtml
		: TagBuilderExt
	{

		/* ctor */


		public SelectInputsHtml(
			string name,
			string[] value,
			RegistryList registry,
			bool isMultiple)
			: base("div", TagRenderMode.Normal)
		{
			Name = name;
			Value = value;
			Registry = registry;
			IsMultiple = isMultiple;
			InnerHtml.AppendLine("");
			if (Registry?.HasItems ?? false)
				InnerHtml.AppendHtml(GetItems());
			else
				InnerHtml.AppendHtml($"{Common.Resources.Common.Text_EmptyItem}");
		}


		/* readonly properties */


		public string Name { get; }
		public string[] Value { get; }
		public RegistryList Registry { get; }
		public bool IsMultiple { get; }


		/* functions */


		public string GetItems()
		{
			if (Registry?.HasItems ?? false)
			{
				var sb1 = new StringBuilder();
				var items1 = IsMultiple
					? Registry.Items.OrderBy(x => x.Value)
					: Registry.Items;
				foreach (var item1 in items1)
				{
					if (item1.IsLabel)
						sb1.AppendLine($"<p>{item1.Value}</p>");
					else
					{
						TagBuilderExt input1 = IsMultiple
							? new CheckboxHtml(
								Name,
								$"{Name}_{item1.Key}",
								item1.Key,
								item1.Value,
								true,
								Value?.Contains(item1.Key) ?? false)
							: new RadioHtml(
								Name,
								item1.Key,
								item1.Value,
								null,
								true,
								Value?.Contains(item1.Key) ?? false);
						sb1.AppendLine(input1.ToString());
					}
				}
				return sb1.ToString();
			}
			return null;
		}

	}

}
