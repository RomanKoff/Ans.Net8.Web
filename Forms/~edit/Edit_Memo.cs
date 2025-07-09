namespace Ans.Net8.Web.Forms
{

	public class Edit_Memo
		: IFormEditControl
	{

		/* ctor */


		public Edit_Memo(
			string name,
			string value,
			string cssClasses = null)
		{
			Name = name;
			Value = value;
			Control = new(Name, Value);
			Control.AddCssClass("form-control");
			Control.AddStyle("border:none;");
			if (cssClasses != null)
				Control.AddCssClass(cssClasses);
			//Control.MergeAttribute("cols", "");
			Control.MergeAttribute("rows", "6");
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }
		public TextareaTag Control { get; }


		/* functions */


		public override string ToString()
		{
			return $"<div class=\"form-control p-0\">{Control}</div>";
		}

	}

}
