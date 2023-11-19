using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Encodings.Web;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
		 * void AddCssClass(this TagBuilder tag, bool expression, string value);
		 * void AddCssClassIfPresent(this TagBuilder tag, string value);
		 * void AddCssClasses(this TagBuilder tag, params string[] classes);
		 * void AddAttibutes(this TagBuilder tag, NameValueCollection attributes);
		 * void AddAttibutes(this TagBuilder tag, object attributes);
		 * void MergeAttribute(this TagBuilder tag, bool expression, string key, string value);
		 * void MergeAttribute(this TagBuilder tag, bool expression, string key);
		 * void MergeAttributeIfPresent(this TagBuilder tag, string key, string value);
		 * void MergeAttributeIfNot0(this TagBuilder tag, string key, int value);
		 * void MergeAttribute(this TagBuilder tag, bool expression, string key, string value, bool replaceExisting);
		 * 
		 * string GetString(this TagBuilder tag);
		 * HtmlString ToHtml(this TagBuilder tag);
         */


		/* methods */


		public static void AddCssClass(
			this TagBuilder tag,
			bool expression,
			string value)
		{
			if (expression)
				tag.AddCssClass(value);
		}


		public static void AddCssClassIfPresent(
			this TagBuilder tag,
			string value)
		{
			if (!string.IsNullOrEmpty(value))
				tag.AddCssClass(value);
		}


		public static void AddCssClasses(
			this TagBuilder tag,
			params string[] classes)
		{
			foreach (string s1 in classes)
				tag.AddCssClassIfPresent(s1);
		}


		public static void AddAttibutes(
			this TagBuilder tag,
			NameValueCollection attributes)
		{
			if (attributes != null)
				foreach (var k1 in attributes.AllKeys)
					tag.MergeAttribute(k1.Replace("__", "-"),
						attributes[k1]);
		}


		public static void AddAttibutes(
			this TagBuilder tag,
			object attributes)
		{
			if (attributes != null)
				foreach (PropertyDescriptor p1 in TypeDescriptor.GetProperties(attributes))
					tag.MergeAttribute(p1.Name.Replace("__", "-"),
						p1.GetValue(attributes).ToString());
		}


		public static void MergeAttribute(
			this TagBuilder tag,
			bool expression,
			string key,
			string value)
		{
			if (expression)
				tag.MergeAttribute(key, value);
		}


		public static void MergeAttribute(
			this TagBuilder tag,
			bool expression,
			string key)
		{
			if (expression)
				tag.MergeAttribute(key, key);
		}


		public static void MergeAttributeIfPresent(
			this TagBuilder tag,
			string key,
			string value)
		{
			if (!string.IsNullOrEmpty(value))
				tag.MergeAttribute(key, value);
		}


		public static void MergeAttributeIfNot0(
			this TagBuilder tag,
			string key,
			int value)
		{
			if (value != 0)
				tag.MergeAttribute(key, value.ToString());
		}


		public static void MergeAttribute(
			this TagBuilder tag,
			bool expression,
			string key,
			string value,
			bool replaceExisting)
		{
			if (expression)
				tag.MergeAttribute(key, value, replaceExisting);
		}


		/* functions */


		public static string GetString(
			this TagBuilder tag)
		{
			var sw1 = new StringWriter();
			tag.WriteTo(sw1, HtmlEncoder.Default);
			return sw1.ToString();
		}


		public static HtmlString ToHtml(
			this TagBuilder tag)
		{
			return new HtmlString(tag.GetString());
		}

	}

}
