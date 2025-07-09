using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Edit_EnumString
		: _Edit_Registry_Base,
		IFormEditControl
	{

		public Edit_EnumString(
			string name,
			string value,
			RegistryList registry,
			string cssClasses = null)
			: base(
				  name,
				  value,
				  registry,
				  RegistryModeEnum.Auto,
				  cssClasses,
				  false)
		{
		}

	}

}
