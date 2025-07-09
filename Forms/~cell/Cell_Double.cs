namespace Ans.Net8.Web.Forms
{

	public class Cell_Double
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Double(
			double? value)
			: base(value.ToString(), true)
		{
		}

	}

}
