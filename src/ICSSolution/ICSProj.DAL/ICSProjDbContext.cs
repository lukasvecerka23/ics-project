using ICSProj.DAL.Entities;
//using ICSProj.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL;

public class ICSProjDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public ICSProjDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
        : base(contextOptions) =>
        _seedDemoData = seedDemoData;

    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

    public DbSet<ProjectAssignEntity> Assigns => Set<ProjectAssignEntity>();

    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();

    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DbSet<TagEntity> Tags => Set<TagEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ActivityEntity>(entity =>
        {
            entity.HasOne(i => i.Project)
                .WithMany(i => i.Activities)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.Tag)
                .WithMany(i => i.Activities)
                .OnDelete(DeleteBehavior.SetNull);

        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasMany(i => i.Activities)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.Cascade);

            /*entity.HasMany(i => i.)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);*/

            entity.HasMany(i => i.ProjectAssigns)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProjectEntity>(entity => 
        {
            entity.HasMany(i => i.ProjectAssigns)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.Users)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.Activities)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Cascade);

        });

    // if (_seedDemoData)
        // {
        //     ActivitySeeds.Seed(modelBuilder);
        //     UserSeeds.Seed(modelBuilder);
        //     ProjectAssignSeeds.Seed(modelBuilder);
        //     ProjectSeeds.Seed(modelBuilder);
        //     TagSeeds.Seed(modelBuilder);
        // }
    }
}