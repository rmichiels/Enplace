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

    public virtual DbSet<DeltaStore> DeltaStores { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientCategory> IngredientCategories { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMenu> UserMenus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:swissknife.database.windows.net,1433;Initial Catalog=EnplaceDB;Persist Security Info=False;User ID=dabman;Password=M4st3rk3y;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbConfig.Configure(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
