namespace Ans.Net8.Web.Forms
{

	public class Cell_Int
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Int(
			int? value)
			: base(value.ToString(), true)
		{
		}

	}

}
