using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Services;
using ICSProj.App.ViewModels;
using ICSProj.App.Views;
using ICSProj.App.Shells;

namespace ICSProj.App;

public static class AppInstaller
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();

        services.AddSingleton<IMessenger>(_ => StrongReferenceMessenger.Default);
        services.AddSingleton<IMessengerService, MessengerService>();

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
            .AsSelf()
            .WithTransientLifetime()
        );

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<IViewModel>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime()
        );

        // services.AddTransient<INavigationService, NavigationService>();

        return services;
    }

}
