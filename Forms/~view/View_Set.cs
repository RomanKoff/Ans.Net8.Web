using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class View_Set
		: IFormViewControl
	{

		private readonly HiddenInputsHtml _hidden;


		/* ctor */


		public View_Set(
			string name,
			int[] value,
			RegistryList registry)
		{
			Name = name;
			Value = value?.Length > 0
				? value.MakeFromCollection(
					x => registry.GetValue(x),
					"<div class=\"d-flex flex-wrap gap-1 lh-sm\">{0}</div>",
					"<div class=\"px-2 py-1 text-dark bg-dark-subtle rounded\">{0}</div>",
					null)
				: $"<div class=\"opacity-50\">{Common.Resources.Common.Text_EmptyItem}</div>";
			ValueData = value.Select(x => x.ToString()).ToArray();
			Control = new(Value);
			_hidden = new(Name, ValueData);
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }
		public string[] ValueData { get; }
		public DivTag Control { get; }


		/* functions */


		public override string ToString()
		{
			return $"{Control}{_hidden}";
		}

	}

}
