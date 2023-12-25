using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
		 * void AddAttributeIfPresent(this TagHelperOutput output, string name, params string[] values);
		 * 
		 * MaxLengthAttribute GetMaxLengthAttribute(this ModelMetadata data);
		 * RegularExpressionAttribute GetRegularExpressionAttribute(this ModelMetadata data);
		 * RangeAttribute GetRangeAttribute(this ModelMetadata data);
		 * RequiredAttribute GetRequiredAttribute(this ModelMetadata data)
         */


		/* methods */


		public static void AddAttributeIfPresent(
			this TagHelperOutput output,
			string name,
			params string[] values)
		{
			var s1 = SuppString.Join(" ", values);
			if (!string.IsNullOrEmpty(s1))
				output.Attributes.Add(name, s1);
		}


		/* functions */


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
