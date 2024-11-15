﻿using Enplace.Service.Entities;
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
        optionsBuilder.UseMySql("Server=192.168.0.139;Port=3306;Database=Enplace;User ID=agent;Password=M4st3rk3y;CharSet=utf8;", ServerVersion.AutoDetect("Server=192.168.0.139;Port=3306;Database=SKID;User ID=agent;Password=M4st3rk3y;CharSet=utf8;"));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbConfig.Configure(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
