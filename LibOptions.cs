using Ans.Net8.Common;
using Ans.Net8.Common.Services;
using Microsoft.Extensions.Configuration;

namespace Ans.Net8.Web
{

	public static partial class _e
	{
		public static LibOptions GetLibOptions(
			this IConfiguration configuration)
		{
			var res1 = configuration
				.GetSection(LibOptions.Name)
					.Get<LibOptions>();
			return res1 ?? throw new Exception(
				$"There is no or empty [{LibOptions.Name}] section in appsettings.json");
		}
	}



	public class LibOptions
	{

		public static readonly string Name
			= "AnsWeb"; //Assembly.GetExecutingAssembly().GetName().Name;


		/* properties */

		public string Culture { get; set; }
		public string CorsProfile { get; set; }
		public string SystemAccessToken { get; set; }
		public string SystemLayout { get; set; }
		public string DefaultCssContainer { get; set; }

		public string DefaultTelCode { get; set; } = "7812";
		public bool UseDeveloperMode { get; set; }
		public bool UseSessions { get; set; }
		public string[] Mimetypes { get; set; }
		public string[] Routes { get; set; }

		public ExceptionHandlerOptions ExceptionHandler { get; set; }
		public SubnetsOptions Subnets { get; set; }
		public MailServiceOptions MailService { get; set; }
		public SsoOptions Sso { get; set; }
		public string[] SocialIcons { get; set; }

	}



	public class ExceptionHandlerOptions
	{
		public string ErrorPath { get; set; }
		public string Error404Path { get; set; }
		public bool ShowInfo { get; set; } = true;
		public string Layout { get; set; }
		public string Picture400 { get; set; }
		public string Picture403 { get; set; }
		public string Picture404 { get; set; }
		public string Picture500 { get; set; }

		public bool HasPicture400
			=> !string.IsNullOrEmpty(Picture400);

		public bool HasPicture403
			=> !string.IsNullOrEmpty(Picture403);

		public bool HasPicture404
			=> !string.IsNullOrEmpty(Picture404);

		public bool HasPicture500
			=> !string.IsNullOrEmpty(Picture500);
	}



	public class SubnetsOptions
	{
		public string Admin { get; set; }
		public string Safe { get; set; }
		public string Unsafe { get; set; }
		public string Allow { get; set; }
		public string Deny { get; set; }

		public IpSubnetsList GetAdminSubnets()
		{
			return (string.IsNullOrEmpty(Admin))
				? null : _adminSubnets ??= new IpSubnetsList(Admin);
		}
		private IpSubnetsList _adminSubnets;

		public IpSubnetsList GetSafeSubnets()
		{
			return (string.IsNullOrEmpty(Safe))
				? null : _safeSubnets ??= new IpSubnetsList(Safe);
		}
		private IpSubnetsList _safeSubnets;

		public IpSubnetsList GetUnsafeSubnets()
		{
			return (string.IsNullOrEmpty(Unsafe))
				? null : _unsafeSubnets ??= new IpSubnetsList(Unsafe);
		}
		private IpSubnetsList _unsafeSubnets;

		public IpSubnetsList GetAllowSubnets()
		{
			return (string.IsNullOrEmpty(Allow))
				? null : _allowSubnets ??= new IpSubnetsList(Allow);
		}
		private IpSubnetsList _allowSubnets;

		public IpSubnetsList GetDenySubnets()
		{
			return (string.IsNullOrEmpty(Deny))
				? null : _denySubnets ??= new IpSubnetsList(Deny);
		}
		private IpSubnetsList _denySubnets;
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



	public class SsoOptions
	{
		public string CookieName { get; set; }
		public bool RequireHttpsMetadata { get; set; }
		public string Authority { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string AppClaimName { get; set; }
		public SsoUsers[] Users { get; set; }
	}



	public class SsoUsers
	{
		public string Username { get; set; }
		public string[] Roles { get; set; }
	}

}