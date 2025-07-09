namespace Ans.Net8.Web.Forms
{

	public class View_Email
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Email(
			string name,
			string value)
			: base(name, value, null, _Consts.MW_Email, false)
		{
		}

	}

}
