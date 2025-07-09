namespace Ans.Net8.Web.Forms
{

	public class View_Text100
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Text100(
			string name,
			string value,
			int? maxWidth = null)
			: base(name, value, null, maxWidth ?? _Consts.MW_Text100, false)
		{
		}

	}

}
