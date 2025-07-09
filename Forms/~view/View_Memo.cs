namespace Ans.Net8.Web.Forms
{

	public class View_Memo
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Memo(
			string name,
			string value)
			: base(name, value, null, 0, false) // _Consts.COLS_Memo
		{
		}

	}

}
