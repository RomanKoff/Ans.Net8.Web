using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public static class Render
	{

		/* 
		 * HtmlString SizeOfKB(long size);
		 * HtmlString SizeOfKB(int size);
		 * HtmlString Join(string templateResult, string templateItem, string itemsSeparator, params string[] data);
		 * HtmlString JoinFromString(string templateResult, string templateItem, string itemsSeparator, string data, string dataSeparator);
		 * 
		 * HtmlString SampleRu();
		 * HtmlString SampleSmallRu();
		 * HtmlString SampleSmallerRu();
		 */


		public static HtmlString SizeOfKB(
			long size)
		{
			return new HtmlString(
				SuppIO.GetSizeOfKB(size).Replace(" ", "&nbsp;"));
		}


		public static HtmlString SizeOfKB(
			int size)
		{
			return SizeOfKB((long)size);
		}


		public static HtmlString Join(
			string templateResult,
			string templateItem,
			string itemsSeparator,
			params string[] data)
		{
			return new HtmlString(
				SuppString.Join(
					templateResult, templateItem, itemsSeparator,
					data));
		}


		public static HtmlString JoinFromString(
			string templateResult,
			string templateItem,
			string itemsSeparator,
			string data,
			string dataSeparator)
		{
			return Join(
				templateResult, templateItem, itemsSeparator,
				data.Split(dataSeparator));
		}


		public static HtmlString SampleRu()
			=> SuppText.SampleRu().ToHtml(true);

		public static HtmlString SampleSmallRu()
			=> SuppText.SampleSmallRu().ToHtml(true);

		public static HtmlString SampleSmallerRu()
			=> SuppText.SampleSmallerRu().ToHtml(true);
	}

}
