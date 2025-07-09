namespace Ans.Net8.Web.Forms
{

	public class View_Float
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Float(
			string name,
			float? value)
			: base(name, value.ToString(), value, _Consts.MW_Float, true)
		{
		}

	}

}
