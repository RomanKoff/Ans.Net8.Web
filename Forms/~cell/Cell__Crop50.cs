using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Cell__Crop50
		: IFormCellControl
	{

		/* ctor */


		public Cell__Crop50(
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
				: SuppTypograph.GetText2Html(Value.GetCrop(0, 50));
		}

	}

}
