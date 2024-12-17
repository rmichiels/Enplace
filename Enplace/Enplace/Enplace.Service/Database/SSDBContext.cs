using Enplace.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service.Database;

public partial class SSDBContext : DbContext
{
    public SSDBContext()
    {
    }

    public SSDBContext(DbContextOptions<SSDBContext> options)
        : base(options)
    {
    }

    private User? _toCommitAs = null;
    public virtual DbSet<DeltaStore> DeltaStores { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientCategory> IngredientCategories { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }
    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public virtual DbSet<RecipeImage> RecipeImages { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMenu> UserMenus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        //optionsBuilder.UseSqlServer("Server=tcp:swissknife.database.windows.net,1433;Initial Catalog=EnplaceDB;Persist Security Info=False;User ID=dabman;Password=M4st3rk3y;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;", options => options.EnableRetryOnFailure());
        optionsBuilder.UseMySql("Server=sql.swissknife.solutions;Port=6446;Database=Enplace;User ID=agent;Password=M4st3rk3y;CharSet=utf8;", 
            ServerVersion.AutoDetect("Server=sql.swissknife.solutions;Port=6446;Database=SKID;User ID=agent;Password=M4st3rk3y;CharSet=utf8;"));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbConfig.Configure(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void CommitAs(User user)
    {
        _toCommitAs = user;
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is TracedEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((TracedEntity)entityEntry.Entity).ModifiedOn = DateTime.Now;
            if (_toCommitAs != null)
            {
                ((TracedEntity)entityEntry.Entity).ModifiedByID = _toCommitAs.Id;
            }
            if (entityEntry.State == EntityState.Added)
            {
                ((TracedEntity)entityEntry.Entity).CreatedOn = DateTime.Now;
                if (_toCommitAs != null)
                {
                    ((TracedEntity)entityEntry.Entity).CreatedByID = _toCommitAs.Id;
                }
            }
        }

        // Release comitting user after writing trace
        _toCommitAs = null;

        return base.SaveChanges();
    }

}
