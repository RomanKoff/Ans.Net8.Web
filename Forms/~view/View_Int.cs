namespace Ans.Net8.Web.Forms
{

	public class View_Int
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Int(
			string name,
			int? value)
			: base(name, value.ToString(), value, _Consts.MW_Int, true)
		{
		}

	}

}
