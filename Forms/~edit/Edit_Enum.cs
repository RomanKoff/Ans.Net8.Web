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
			RegistryModeEnum mode,
			string cssClasses)
			: base(
				  name,
				  value.ToString(),
				  registry,
				  mode,
				  cssClasses,
				  false)
		{
		}


		public Edit_Enum(
			string name,
			int value,
			RegistryList registry,
			string cssClasses = null)
			: this(
				  name,
				  value,
				  registry,
				  RegistryModeEnum.Auto,
				  cssClasses)
		{
		}

	}

}
