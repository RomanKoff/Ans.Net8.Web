namespace Ans.Net8.Web.Forms
{

	public class View_Text250
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Text250(
			string name,
			string value,
			int? maxWidth = null)
			: base(name, value, null, maxWidth ?? _Consts.MW_Text250, false)
		{
		}

	}

}
