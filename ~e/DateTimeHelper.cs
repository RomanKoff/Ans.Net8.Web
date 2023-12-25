using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
         * HtmlString GetPassedHtml(this DateTimeHelper helper, DateTime datetime, bool addTime, bool useYesterdayTodayTomorrow = true);
         * HtmlString GetPassedHtml(this DateTimeHelper helper, DateTime? datetime, bool addTime, bool useYesterdayTodayTomorrow = true);
         * HtmlString GetSpanHtml(this DateTimeHelper helper, DateTime date1, DateTime? date2, bool showCurrentYear = false, bool useYesterdayTodayTomorrow = false);
         */


		public static HtmlString GetPassedHtml(
			this DateTimeHelper helper,
			DateTime datetime,
			bool addTime,
			bool useYesterdayTodayTomorrow = true)
		{
			return helper.GetPassed(
				datetime, addTime, useYesterdayTodayTomorrow)
					.ToHtml();
		}


		public static HtmlString GetPassedHtml(
			this DateTimeHelper helper,
			DateOnly date,
			bool useYesterdayTodayTomorrow = true)
		{
			return helper.GetPassedHtml(
				date.GetDateTime(), false, useYesterdayTodayTomorrow);
		}


		public static HtmlString GetPassedHtml(
			this DateTimeHelper helper,
			DateTime? datetime,
			bool addTime,
			bool useYesterdayTodayTomorrow = true)
		{
			return helper.GetPassed(
				datetime, addTime, useYesterdayTodayTomorrow)
					.ToHtml();
		}


		public static HtmlString GetPassedHtml(
			this DateTimeHelper helper,
			DateOnly? date,
			bool useYesterdayTodayTomorrow = true)
		{
			return helper.GetPassedHtml(
				date?.GetDateTime(), false, useYesterdayTodayTomorrow);
		}

	}

}
