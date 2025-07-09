using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Cell__Link
		: IFormCellControl
	{

		/* ctor */


		public Cell__Link(
			string value)
		{
			Value = value;
		}


		/* readonly properties */


		public string Value { get; }


		/* functions */


		public override string ToString()
		{
			return string.IsNullOrEmpty(Value)
				? "&nbsp;"
				: $"<a class=\"text-break\" target=\"_blank\" href=\"{Value}\">{Value.GetCrop(0, 50)}</a>";
		}

	}

}
