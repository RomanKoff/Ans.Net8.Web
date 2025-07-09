using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Edit_Enum
		: _Edit_Registry_Base,
		IFormEditControl
	{

		public Edit_Enum(
			string name,
			int value,
			RegistryList registry,
			string cssClasses = null)
			: base(
				  name,
				  value.ToString(),
				  registry,
				  RegistryModeEnum.Auto,
				  cssClasses,
				  false)
		{
		}

	}

}
