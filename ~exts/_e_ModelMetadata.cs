using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Ans.Net8.Web
{

	public static partial class _e_ModelMetadata
	{

		public static MaxLengthAttribute GetMaxLengthAttribute(
			this ModelMetadata data)
		{
			return data.ValidatorMetadata
				.OfType<MaxLengthAttribute>()
				.FirstOrDefault();
		}


		public static RegularExpressionAttribute GetRegularExpressionAttribute(
			this ModelMetadata data)
		{
			return data.ValidatorMetadata
				.OfType<RegularExpressionAttribute>()
				.FirstOrDefault();
		}


		public static RangeAttribute GetRangeAttribute(
			this ModelMetadata data)
		{
			return data.ValidatorMetadata
				.OfType<RangeAttribute>()
				.FirstOrDefault();
		}


		public static RequiredAttribute GetRequiredAttribute(
			this ModelMetadata data)
		{
			return data.ValidatorMetadata
				.OfType<RequiredAttribute>()
				.FirstOrDefault();
		}

	}

}
