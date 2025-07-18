using AspNetCoreHero.ToastNotification;
using ESG.Common.Services.Shared;
using ESG.Common.Web.Utility.Annotations;
using ESG.Common.Web.Utility.Authorization;
using ESG.Common.Web.Utility.Logging;
using ESG.IDMS.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Razor;
using Quartz;
using System.Globalization;
using System.Reflection;
using ESG.IDMS.Web.Service;
using ESG.IDMS.Application.Helpers;

namespace ESG.IDMS.Web;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDefaultServices(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddApplicationInsightsTelemetry();
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddControllersWithViews();
        services.ConfigureSecurity(Configuration);
        services.ConfigureAuthorization();
        services.ConfigureLocalization();
        services.AddRazorPages().ConfigureLocalization();
        services.AddApplicationServices();
        services.AddQuartz();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(configuration =>
		{           
			configuration.RegisterServicesFromAssemblies([Assembly.GetExecutingAssembly()]);
		});
        services.AddWebOptimizer();
        services.AddNotyf(config =>
        {
            config.DurationInSeconds = 10;
            config.IsDismissable = true;
            config.Position = NotyfPosition.TopRight;
        });
        services.AddLogEnricherServices();
        services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
		services.AddTransient<DropdownServices>();
		DateHelper.TimeOffset = Configuration.GetValue<int>("TimeOffset");
		services.AddTransient<AIDataAnalyticsServices>();
		services.AddTransient<AIReportQueryGenerationServices>();
        services.Configure<List<Settings.ApplicationEcosystem>>(Configuration.GetSection("ApplicationEcosystem"));
    }

    public static IServiceCollection ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.Secure = CookieSecurePolicy.Always;
        });
        services.AddAntiforgery(options =>
        {
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
        services.Configure<CookieTempDataProviderOptions>(options =>
        {
            options.Cookie.IsEssential = true;
        });
        var tokenLifespan = configuration.GetValue<double>("TokenLifespan", 1);
        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(tokenLifespan);
        });
        return services;
    }

    public static IServiceCollection ConfigureLocalization(this IServiceCollection services)
    {
        var enUSCulture = "en-US";
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo(enUSCulture),
                new CultureInfo("zh-CN")
                };

            options.DefaultRequestCulture = new RequestCulture(culture: enUSCulture, uiCulture: enUSCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        return services;
    }

    public static IMvcBuilder ConfigureLocalization(this IMvcBuilder builder) =>
        builder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
        });

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
		services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        return services;
    }
}