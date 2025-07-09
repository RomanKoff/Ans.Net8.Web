using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class View_Bool
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Bool(
			string name,
			bool value)
			: base(
				  name,
				  value.Make(
					  Resources.Common.Html_ViewTrue,
					  Resources.Common.Html_ViewFalse),
				  value,
				  _Consts.MW_Bool,
				  true)
		{
		}

	}

}
