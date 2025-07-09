namespace Ans.Net8.Web.Forms
{

	public class View_Name
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Name(
			string name,
			string value)
			: base(name, value, null, _Consts.MW_Name, false)
		{
		}

	}

}
