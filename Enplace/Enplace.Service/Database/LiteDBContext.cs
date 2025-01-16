using Enplace.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
    public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }
    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public virtual DbSet<RecipeImage> RecipeImages { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMenu> UserMenus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        string directory = Directory.GetCurrentDirectory();
        string liteConstr = Path.Combine([directory, "Enplace.db"]);
        Debug.WriteLine(liteConstr);
        optionsBuilder.UseSqlite($"Data Source={Path.Combine([directory, "Enplace.db"])};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbConfig.Configure(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
