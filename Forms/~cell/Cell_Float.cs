namespace Ans.Net8.Web.Forms
{

	public class Cell_Float
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Float(
			float? value)
			: base(value.ToString(), true)
		{
		}

	}

}
