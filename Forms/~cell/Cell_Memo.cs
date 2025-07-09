namespace Ans.Net8.Web.Forms
{

	public class Cell_Memo
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Memo(
			string value)
			: base(value, false)
		{
		}

	}

}
