using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class View_Enum
		: _View_Reference_Base,
		IFormViewControl
	{

		public View_Enum(
			string name,
			int value,
			RegistryList registry)
			: base(
				  name,
				  value.ToString(),
				  registry)
		{
		}

	}

}
