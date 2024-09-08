using Enplace.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service.Database
{
    public static class DbConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeltaStore>(entity =>
            {
                entity.ToTable("DeltaStore");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasIndex(e => e.Name, "idx_Ingredients_NameAsc").IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IngredientCategoryId).HasColumnName("IngredientCategoryID");
                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.IngredientCategory).WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.IngredientCategoryId)
                    .HasConstraintName("fk_Ingredients_N1_IngredientCategories");
                entity.Navigation(e => e.IngredientCategory).AutoInclude();
            });

            modelBuilder.Entity<IngredientCategory>(entity =>
            {
                entity.HasIndex(e => e.Name, "idx_IngredientCategories_NameAsc").IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.HasIndex(e => e.Name, "idx_Measurements_NameAsc").IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.OwnerUserId).HasColumnName("OwnerUserID");
                entity.Property(e => e.RecipeCategoryId).HasColumnName("RecipeCategoryID");

                entity.HasOne(d => d.OwnerUser).WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.OwnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipes_N1_Users");

                entity.HasMany(e => e.RecipeImages).WithOne(i => i.Recipe)
                .HasForeignKey(e => e.RecipeId)
                .HasConstraintName("fk_RecipeImages_N1_Recipe");

                entity.Navigation(e => e.RecipeIngredients).AutoInclude();
                entity.Navigation(e => e.RecipeSteps).AutoInclude();
                entity.Navigation(e => e.Category).AutoInclude();
            });

            modelBuilder.Entity<RecipeCategory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Recipes).WithOne(r => r.Category)
                .HasForeignKey(r => r.RecipeCategoryId)
                .HasConstraintName("fk_Recipes_N1_RecipeCategory");
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId });

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");
                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 4)");

                entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .HasConstraintName("fk_RecipeIngredients_N1_Ingredients");

                entity.HasOne(d => d.Measurement).WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.MeasurementId)
                    .HasConstraintName("fk_RecipeIngredients_N1_Measurements");

                entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("fk_RecipeIngredients_N1_Recipes");

                entity.Navigation(e => e.Measurement).AutoInclude();
                entity.Navigation(e => e.Ingredient).AutoInclude();
            });

            modelBuilder.Entity<RecipeImage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Recipe).WithMany(r => r.RecipeImages).HasForeignKey(e => e.RecipeId);
            });

            modelBuilder.Entity<RecipeStep>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.RecipeId });

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeSteps)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("fk_RecipeSteps_N1_Recipes");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasMany(d => d.RecipesNavigation).WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRecipe",
                        r => r.HasOne<Recipe>().WithMany()
                            .HasForeignKey("RecipeId")
                            .HasConstraintName("fk_UserRecipes_N1_Recipes"),
                        l => l.HasOne<User>().WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_UserRecipes_N1_Users"),
                        j =>
                        {
                            j.HasKey("UserId", "RecipeId");
                            j.ToTable("UserRecipes");
                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                            j.IndexerProperty<int>("RecipeId").HasColumnName("RecipeID");
                        });
            });

            modelBuilder.Entity<UserMenu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.UserMenus)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserMenus_N1_Users");

                entity.HasMany(d => d.Recipes).WithMany(p => p.Menus)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserMenuRecipe",
                        r => r.HasOne<Recipe>().WithMany()
                            .HasForeignKey("RecipeId")
                            .HasConstraintName("fk_UserMenuRecipes_N1_Recipes"),
                        l => l.HasOne<UserMenu>().WithMany()
                            .HasForeignKey("MenuId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_UserMenuRecipes_N1_UserMenus"),
                        j =>
                        {
                            j.HasKey("MenuId", "RecipeId").HasName("PK_MenuRecipes");
                            j.ToTable("UserMenuRecipes");
                            j.IndexerProperty<int>("MenuId").HasColumnName("MenuID");
                            j.IndexerProperty<int>("RecipeId").HasColumnName("RecipeID");
                        });
            });
        }
    }
}
