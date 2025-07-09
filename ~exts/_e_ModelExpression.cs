using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ans.Net8.Web
{

	public static partial class _e_ModelExpression
	{

		public static string GetModelValueString(
			this ModelExpression expression)
		{
			var value1 = expression.Model;
			if (value1 == null)
				return null;
			return SuppValues.GetValueStringForWeb(value1);
		}

	}

}
