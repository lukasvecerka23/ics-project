using CommunityToolkit.Maui;
using ICSProj.App.Services;
using ICSProj.BL;

namespace ICSProj.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-ExtraLight.ttf", "PoppinsExtraLight");
            });

        builder.Services
            .AddDALServices()
            .AddBLServices()
            .AddAppServices();

        var app = builder.Build();

        app.Services.GetRequiredService<IDbMigrator>().Migrate();
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());

        return app;
    }

    private static void RegisterRouting(INavigationService navigationService)
    {
        foreach (var route in navigationService.Routes)
        {
            Console.WriteLine(route.Route);
            Console.WriteLine(route.ViewType);
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }
}
