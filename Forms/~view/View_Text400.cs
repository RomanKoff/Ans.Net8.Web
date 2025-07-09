namespace Ans.Net8.Web.Forms
{

	public class View_Text400
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Text400(
			string name,
			string value,
			int? maxWidth = null)
			: base(name, value, null, maxWidth ?? _Consts.MW_Text400, false)
		{
		}

	}

}
