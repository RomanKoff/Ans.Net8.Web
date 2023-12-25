using Ans.Net8.Common.Services;
using Ans.Net8.Web.Middlewares;
using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace Ans.Net8.Web
{

	public static class LibStartup
	{

		/*
         * IMvcBuilder Add_AnsWeb(this WebApplicationBuilder builder, IConfiguration configuration);
         * void Use_AnsWeb(this WebApplication app, IConfiguration configuration);
         * void Use_AnsWeb_Nodes(this WebApplication app);
         */


		private static LibOptions _options;


		/* functions */


		public static IMvcBuilder Add_AnsWeb(
			this WebApplicationBuilder builder,
			IConfiguration configuration)
		{
			_options = configuration.GetLibOptions();
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			/* IServiceCollection */

			builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
			builder.Services.AddSingleton<IAnsCacheMap, AnsCacheMap>();
			builder.Services.AddSingleton<IMapNodesProvider, MapNodesProvider_Xml>();
			builder.Services.AddSingleton<IMapPagesProvider, MapPagesProvider_Xml>();
			builder.Services.AddScoped<IViewRenderService, ViewRenderService_Ans>();
			builder.Services.AddScoped<ICurrentContext, CurrentContext>();
			builder.Services.AddScoped(
				x => x.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(
					x.GetRequiredService<IActionContextAccessor>().ActionContext));

			builder.Services.AddResponseCaching();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddHttpClient();
			builder.Services.AddCors(
				o => o.AddPolicy(_Consts.CORS_POLICY_ALLOW_ALL, p =>
				{
					p.AllowAnyOrigin();
					p.AllowAnyHeader();
					p.AllowAnyMethod();
				}));
			if (_options.UseSessions)
			{
				builder.Services.AddDistributedMemoryCache();
				builder.Services.AddSession();
			}

			/* IMvcBuilder */

			var builder1 = builder.Services.AddMvc(o =>
			{
				o.CacheProfiles.Add("D60", _Consts.CACHE_PROFILE_D60);
				o.CacheProfiles.Add("D30", _Consts.CACHE_PROFILE_D30);
			});
			builder.Services.Configure<RequestLocalizationOptions>(o =>
			{
				var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ru") };
				o.DefaultRequestCulture = new RequestCulture("ru", "ru");
				o.SupportedCultures = supportedCultures;
				o.SupportedUICultures = supportedCultures;
			});
			builder1.AddJsonOptions(o =>
			{
				o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				//o.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
				//o.JsonSerializerOptions.Converters.Insert(0, new BoolConverter());
				//o.JsonSerializerOptions.Converters.Insert(0, new AutoNumberToStringConverter());
				//o.JsonSerializerOptions.Converters.Insert(0, new AutoStringToNumberConverter());
			});
			if (_options.Sso != null)
			{
				builder._addSsoAuthentication();
				builder._addSsoAuthorization();
			}
			return builder1;
		}


		public static void Use_AnsWeb(
			this WebApplication app)
		{
			if (_options.UseDeveloperMode)
			{
				app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
			}
			else
			{
				app.UseExceptionHandler(
					_options.ExceptionHandler?.ErrorPath ?? "/Errors/ServerError");
				app.UseStatusCodePagesWithReExecute(
					_options.ExceptionHandler?.Error404Path ?? "/Errors/HttpErrors", "?code={0}");
			}
			if (!app.Environment.IsDevelopment())
				app.UseHsts();
			app.UseHttpsRedirection();
			if (_options.Mimetypes?.Length > 0)
			{
				var provider1 = new FileExtensionContentTypeProvider();
				foreach (var item1 in _options.Mimetypes)
				{
					var a1 = item1.Split('|');
					provider1.Mappings[a1[0]] = a1[1];
				}
				app.UseStaticFiles(new StaticFileOptions
				{
					ContentTypeProvider = provider1,
				});
			}
			else
			{
				app.UseStaticFiles();
			}
			app.UseResponseCaching();
			app.UseCors(_options.CorsProfile ?? _Consts.CORS_POLICY_ALLOW_ALL);
			if (_options.UseSessions)
				app.UseSession();
			app.UseRouting();
			if (_options.Sso != null)
				app.UseAuthorization();
			app.MapControllers();
			app.MapRazorPages();
			if (_options.Routes?.Length > 0)
				app.AddRoutes(_options.Routes);
			app.UseMiddleware<AnsCultureMiddleware>();
		}


		/* privates */


		private static void _addSsoAuthentication(
			this WebApplicationBuilder builder)
		{
			builder.Services.AddTransient<IClaimsTransformation, ClaimsRoles_Ans>();
			if (_options.Sso.Users != null)
				builder.Services.AddScoped<IRolesProvider, RolesProvider_Ans>();
			var auth1 = builder.Services.AddAuthentication(o =>
			{
				o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			});
			auth1.AddCookie(o =>
			{
				if (_options.Sso.CookieName != null)
					o.Cookie.Name = _options.Sso.CookieName;
				o.Cookie.MaxAge = TimeSpan.FromMinutes(60);
				o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
				o.SlidingExpiration = true;
				o.AccessDeniedPath = new PathString("/SSO/AccessDenied");
			});
			auth1.AddOpenIdConnect(o =>
			{
				o.RequireHttpsMetadata = _options.Sso.RequireHttpsMetadata;
				o.Authority = _options.Sso.Authority;
				o.ClientId = _options.Sso.ClientId;
				o.ClientSecret = _options.Sso.ClientSecret;
				o.ResponseType = OpenIdConnectResponseType.CodeIdToken;
				o.GetClaimsFromUserInfoEndpoint = true;
				o.Scope.Add("openid");
				o.Scope.Add("profile");
				o.Scope.Add("roles");
				o.Scope.Add("email");
				o.SaveTokens = true;
				o.TokenValidationParameters.NameClaimType = "preferred_username";
				o.Events = new OpenIdConnectEvents
				{
					OnRedirectToIdentityProviderForSignOut = x =>
					{
						Debug.WriteLine($"OnAuthorizationCodeReceived");
						return Task.CompletedTask;
					},
					OnAuthorizationCodeReceived = x =>
					{
						Debug.WriteLine($"OnAuthorizationCodeReceived '{x.Principal?.Identity?.Name}'");
						return Task.CompletedTask;
					},
					OnRedirectToIdentityProvider = x =>
					{
						Debug.WriteLine($"OnRedirectToIdentityProvider");
						return Task.CompletedTask;
					},
					OnMessageReceived = x =>
					{
						Debug.WriteLine($"OnMessageReceived '{x.Principal?.Identity?.Name}'");
						return Task.CompletedTask;
					},
					OnSignedOutCallbackRedirect = x =>
					{
						Debug.WriteLine($"OnSignedOutCallbackRedirect '{x.Principal?.Identity?.Name}'");
						return Task.CompletedTask;
					},
					OnTicketReceived = x =>
					{
						Debug.WriteLine($"OnTicketReceived '{x.Principal?.Identity?.Name}'");
						return Task.CompletedTask;
					},
					OnTokenResponseReceived = x =>
					{
						Debug.WriteLine($"OnTokenResponseReceived '{x.Principal?.Identity?.Name}'");
						return Task.CompletedTask;
					},
					OnTokenValidated = x =>
					{
						Debug.WriteLine($"OnTokenValidated '{x.Principal?.Identity?.Name}'");
						return Task.CompletedTask;
					},
					OnUserInformationReceived = x =>
					{
						Debug.WriteLine($"OnUserInformationReceived '{x.Principal?.Identity?.Name}'");
						return Task.CompletedTask;
					},
				};
			});
		}


		private static void _addSsoAuthorization(
			this WebApplicationBuilder builder)
		{
			if (!string.IsNullOrEmpty(_options.Sso.AppClaimName))
			{
				_ = builder.Services.AddAuthorization(o =>
				{
					o.AddPolicy(_Consts.AUTH_POLICY_APP, x => x.RequireClaim(
						_options.Sso.AppClaimName));
					o.AddPolicy(_Consts.AUTH_POLICY_APP_ADMINS, x => x.RequireClaim(
						_options.Sso.AppClaimName,
						_getValues("admin")));
					o.AddPolicy(_Consts.AUTH_POLICY_APP_MODERATORS, x => x.RequireClaim(
						_options.Sso.AppClaimName,
						_getValues("admin;moderator")));
					o.AddPolicy(_Consts.AUTH_POLICY_APP_EDITORS, x => x.RequireClaim(
						_options.Sso.AppClaimName,
						_getValues("admin;moderator;editor")));
					o.AddPolicy(_Consts.AUTH_POLICY_APP_READERS, x => x.RequireClaim(
						_options.Sso.AppClaimName,
						_getValues("admin;moderator;editor;reader")));
					o.AddPolicy(_Consts.AUTH_POLICY_APP_USERS, x => x.RequireClaim(
						_options.Sso.AppClaimName,
						_getValues("admin;moderator;editor;reader;user")));
				});
			}
		}


		private static IEnumerable<string> _getValues(
			string items)
		{
			return items.Split(';')
				.Select(x => $"{{\"right\":\"{x}\"}}");
		}

	}

}
