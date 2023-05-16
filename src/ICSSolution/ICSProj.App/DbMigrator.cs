using ICSProj.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.App;

interface IDbMigrator
{
    public void Migrate();
    public Task MigrateAsync(CancellationToken cancellationToken);
}

public class SqliteDbMigrator: IDbMigrator
{
    private readonly IDbContextFactory<ICSProjDbContext> _dbContextFactory;

    public SqliteDbMigrator(IDbContextFactory<ICSProjDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using ICSProjDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        // await dbContext.Database.MigrateAsync(cancellationToken);

        await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
