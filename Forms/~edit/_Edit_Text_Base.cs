namespace Ans.Net8.Web.Forms
{

	public class _Edit_Text_Base
		: IFormEditControl
	{

		/* ctor */


		public _Edit_Text_Base(
			string name,
			string value,
			string cssClasses,
			int maxWidth)
		{
			Name = name;
			Value = value;
			MaxWidth = maxWidth;
			Control = new(Name, Value);
			Control.AddCssClass("form-control");
			if (cssClasses != null)
				Control.AddCssClass(cssClasses);
			if (MaxWidth > 0)
				Control.AddStyle($"max-width:{MaxWidth}rem;");
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }
		public int MaxWidth { get; }
		public InputTextTag Control { get; }


		/* functions */


		public override string ToString()
		{
			return $"{Control}";
		}

	}

}
