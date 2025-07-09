using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class InputRadioTag
		: TagBuilderExt
	{

		public InputRadioTag(
			string name,
			string id,
			string value,
			bool isChecked)
			: base("input", TagRenderMode.SelfClosing)
		{
			Name = name;
			Id = id;
			Value = value;
			IsChecked = isChecked;
			AddCssClass("form-check-input");
			MergeAttribute("name", Name);
			MergeAttribute("id", Id);
			MergeAttribute("type", "radio");
			MergeAttribute("value", Value);
			if (IsChecked)
				MergeAttribute("checked", "checked");
		}


		/* readonly properties */


		public string Name { get; }
		public string Id { get; }
		public string Value { get; }
		public bool IsChecked { get; }

	}

}
