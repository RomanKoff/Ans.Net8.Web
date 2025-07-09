namespace Ans.Net8.Web.Forms
{

	public class View_Varname
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Varname(
			string name,
			string value,
			int? maxWidth = null)
			: base(name, value, null, maxWidth ?? _Consts.MW_Varname, false)
		{
		}

	}

}
