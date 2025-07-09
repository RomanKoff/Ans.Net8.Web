using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class _Edit_Registry_Base
		: IFormEditControl
	{

		/* ctor */


		public _Edit_Registry_Base(
			string name,
			string value,
			RegistryList registry,
			RegistryModeEnum mode,
			string cssClasses,
			bool isMultiple)
		{
			Name = name;
			Value = value;
			Registry = registry;
			Mode = mode == RegistryModeEnum.Auto
				? registry.GetProposeMode()
				: mode;
			CssClasses = cssClasses;
			IsMultiple = isMultiple;
			Control = GetControl();
			if (cssClasses != null)
				Control.AddCssClass(cssClasses);
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }
		public RegistryList Registry { get; }
		public RegistryModeEnum Mode { get; }
		public string CssClasses { get; }
		public TagBuilderExt Control { get; }
		public bool IsMultiple { get; }


		/* functions */


		public override string ToString()
		{
			return Mode == RegistryModeEnum.Select
				? $"<div class=\"p-0\" style=\"max-width:{Registry.GetMaxWidth()}rem;\">{Control}</div>"
				: $"{Control}";
		}


		public TagBuilderExt GetControl()
		{
			TagBuilderExt ctrl1;
			switch (Mode)
			{
				case RegistryModeEnum.Inputs:
					ctrl1 = new SelectInputsHtml(
						Name,
						Value?.Split(Common._Consts.SEPS_ARRAY),
						Registry,
						IsMultiple);
					break;
				default:
					ctrl1 = new SelectTag(
						Name,
						Value?.Split(Common._Consts.SEPS_ARRAY),
						Registry,
						IsMultiple);
					ctrl1.AddCssClass(
						"form-select guap-tom-select");
					break;
			}
			ctrl1.MergeAttribute("data-value", Value);
			return ctrl1;
		}

	}

}
