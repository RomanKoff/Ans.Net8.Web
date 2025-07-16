using Ans.Net8.Common;
using Ans.Net8.Common.Services;
using Microsoft.Extensions.Configuration;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		private static LibOptions _libOptions;
		public static LibOptions GetOptions_AnsNet8Web(
			this IConfiguration configuration)
		{
			return _libOptions ??= configuration.GetOptions<LibOptions>("Ans.Net8.Web");
		}

	}



	public class LibOptions
		: _AppSettingsOptions_Proto
	{
		public override void Test()
		{
			if (DefaultLayout == null)
				throw GetExceptionParamRequired(nameof(DefaultLayout));
			if (SystemToken == null)
				throw GetExceptionParamRequired(nameof(SystemToken));
		}

		public string Culture { get; set; } = "ru";
		public string DefaultTelCode { get; set; } = "+7-812";
		public string DefaultCssContainer { get; set; }
		public string DefaultLayout { get; set; }
		public string SystemLayout { get; set; }
		public string CorsProfile { get; set; }
		public bool UseDeveloperMode { get; set; }
		public bool UseRuntimeCompilation { get; set; }
		public bool UseSessions { get; set; }
		public string SystemToken { get; set; }
		public string[] Mimetypes { get; set; }
		public string[] Routes { get; set; }
		public ErrorsOptions Errors { get; set; }
		public SubnetsOptions Subnets { get; set; }
		public MailServiceOptions MailService { get; set; }
		public Dictionary<string, Dictionary<string, string>> Regs { get; set; }
	}



	public class ErrorsOptions
	{
		public string Layout { get; set; }
		public string ServerErrorPath { get; set; }
		public string HttpErrorPath { get; set; }
		public bool ShowInfo { get; set; }
		public string Picture400 { get; set; }
		public string Picture403 { get; set; }
		public string Picture404 { get; set; }
		public string Picture500 { get; set; }
	}



	public class SubnetsOptions
	{
		public string Admin { get; set; }
		public string Safe { get; set; }
		public string Unsafe { get; set; }
		public string Allow { get; set; }
		public string Deny { get; set; }

		private IPSubnetsList _adminSubnets;
		public IPSubnetsList GetAdminSubnets()
		{
			return (string.IsNullOrEmpty(Admin))
				? null : _adminSubnets ??= new IPSubnetsList(Admin);
		}

		private IPSubnetsList _safeSubnets;
		public IPSubnetsList GetSafeSubnets()
		{
			return (string.IsNullOrEmpty(Safe))
				? null : _safeSubnets ??= new IPSubnetsList(Safe);
		}

		private IPSubnetsList _unsafeSubnets;
		public IPSubnetsList GetUnsafeSubnets()
		{
			return (string.IsNullOrEmpty(Unsafe))
				? null : _unsafeSubnets ??= new IPSubnetsList(Unsafe);
		}

		private IPSubnetsList _allowSubnets;
		public IPSubnetsList GetAllowSubnets()
		{
			return (string.IsNullOrEmpty(Allow))
			? null : _allowSubnets ??= new IPSubnetsList(Allow);
		}

		private IPSubnetsList _denySubnets;
		public IPSubnetsList GetDenySubnets()
		{
			return (string.IsNullOrEmpty(Deny))
				? null : _denySubnets ??= new IPSubnetsList(Deny);
		}
	}



	public class MailServiceOptions
		: IMailerServiceOptions
	{
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public bool SmtpUseSsl { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public string DefaultFromAddress { get; set; }
		public string DefaultFromTitle { get; set; }
		public string DebugCc { get; set; }
	}

}
