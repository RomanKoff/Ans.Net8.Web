using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Edit_Bool
		: IFormEditControl
	{

		/* ctor */


		public Edit_Bool(
			string name,
			bool value,
			string title = null)
		{
			Name = name;
			Value = value;
			Title = title ?? Resources.Common.Html_EditChecked;
			Control = new(Name, Name, true.ToString(), Title, true, Value);
		}


		/* readonly properties */


		public string Name { get; }
		public bool Value { get; }
		public string Title { get; }
		public CheckboxHtml Control { get; }


		/* functions */


		public override string ToString()
		{
			return $"<div>{Control}</div>";
		}

	}

}
