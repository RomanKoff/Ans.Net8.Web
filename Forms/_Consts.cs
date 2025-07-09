using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public static partial class _Consts
	{

		public static readonly RegistryList REG_Bool = new(
			$"{true}={Resources.Common.Html_EditTrue};{false}={Resources.Common.Html_EditFalse}");


		public const int MW_Text50 = 15;
		public const int MW_Text100 = 25;
		public const int MW_Text250 = 40;
		public const int MW_Text400 = 80;
		public const int COLS_Memo = 100;
		public const int MW_Name = 12;
		public const int MW_Varname = MW_Name;
		public const int MW_Email = 16;
		public const int MW_Int = 8;
		public const int MW_Long = 10;
		public const int MW_Float = MW_Long;
		public const int MW_Double = MW_Long;
		public const int MW_Decimal = MW_Long;
		public const int MW_DateTime = 11;
		public const int MW_DateOnly = 9;
		public const int MW_TimeOnly = 6;
		public const int MW_Bool = 5;
		//public const int MW_Enum = 100;
		//public const int MW_Set = 100;
		//public const int MW_Reference = 100;

	}

}
