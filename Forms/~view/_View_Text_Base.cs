using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class _View_Text_Base
		: IFormViewControl
	{

		private readonly InputHiddenTag _hidden;


		/* ctor */


		public _View_Text_Base(
			string name,
			string valueText,
			object valueData,
			int maxWidth,
			bool useRaw)
		{
			Name = name;
			Value = valueText;
			ValueData = SuppValues.GetValueStringForWeb(valueData) ?? valueText;
			MaxWidth = maxWidth;
			UseRaw = useRaw;
			var s1 = useRaw
				? Value : SuppTypograph.GetText2Html(Value);
			Control = new(s1);
			Control.AddCssClass("form-control bg-light text-dark");
			if (MaxWidth > 0)
				Control.AddStyle($"max-width:{MaxWidth}rem;");
			_hidden = new(Name, ValueData);
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }
		public string ValueData { get; }
		public int MaxWidth { get; }
		public bool UseRaw { get; }
		public DivTag Control { get; }


		/* functions */


		public override string ToString()
		{
			return $"{Control}{_hidden}";
		}

	}

}
