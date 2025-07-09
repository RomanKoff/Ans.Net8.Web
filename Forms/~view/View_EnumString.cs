using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class View_EnumString
		: _View_Reference_Base,
		IFormViewControl
	{

		public View_EnumString(
			string name,
			string value,
			RegistryList registry)
			: base(
				  name,
				  value,
				  registry)
		{
		}

	}

}
