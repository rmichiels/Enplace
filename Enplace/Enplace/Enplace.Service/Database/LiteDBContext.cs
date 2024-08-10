using Enplace.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service.Database;

public partial class LiteDBContext : DbContext
{
    public LiteDBContext()
    {
    }

    public LiteDBContext(DbContextOptions<SSDBContext> options)
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
    {
        string directory = Directory.GetCurrentDirectory();
        string liteConstr = $"Data Source={directory}\\Enplace.db;";
        optionsBuilder.UseSqlite(liteConstr);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbConfig.Configure(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
