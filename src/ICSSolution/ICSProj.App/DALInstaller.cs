using ICSProj.DAL;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.App;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services)
    {
        string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, "ICSProj.db");
        services.AddSingleton<IDbContextFactory<ICSProjDbContext>>(provider => new DbContextSqLiteFactory(databaseFilePath, true));
        services.AddSingleton<IDbMigrator, SqliteDbMigrator>();

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<ProjectAssignEntityMapper>();
        services.AddSingleton<ProjectEntityMapper>();
        services.AddSingleton<TagEntityMapper>();
        services.AddSingleton<UserEntityMapper>();

        return services;
    }

}
