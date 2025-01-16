using Enplace.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service.Database
{
    public static class DbConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasKey(log => new { log.Topic, log.ItemID, log.UserID });

                entity.HasOne(l => l.User).WithMany(l => l.ActivityLog);
            });


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

                entity.HasOne(d => d.OwnerUser).WithMany(p => p.OwnedRecipes)
                    .HasForeignKey(d => d.OwnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipes_N1_Users");

                entity.HasMany(e => e.RecipeImages).WithOne(i => i.Recipe)
                .HasForeignKey(e => e.RecipeId)
                .HasConstraintName("fk_RecipeImages_N1_Recipe");

                entity.Navigation(e => e.RecipeIngredients).AutoInclude();
                entity.Navigation(e => e.RecipeSteps).AutoInclude();
                entity.Navigation(e => e.Category).AutoInclude();
                entity.Navigation(e => e.OwnerUser).AutoInclude();
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
                entity.HasKey(e => new { e.Id });

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
                entity.HasIndex(e => e.SKID).IsUnique();
                entity.HasIndex(e => e.Name).IsUnique();

                entity.HasMany(u => u.ActivityLog)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserID);
            });

            modelBuilder.Entity<UserMenu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.UserMenus)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserMenus_N1_Users");
            });

            modelBuilder.Entity<UserMenuRecipe>(entity =>
            {
                entity.ToTable("UserMenuRecipes");
                entity.HasKey(umr => new { umr.MenuID, umr.RecipeID });

                entity.HasOne(umr => umr.Recipe)
                .WithMany(r => r.MenuRecipes)
                .HasForeignKey(umr => umr.RecipeID);

                entity.HasOne(umr => umr.Menu)
                .WithMany(m => m.MenuRecipes)
                .HasForeignKey(umr => umr.MenuID);
            });

            modelBuilder.Entity<UserRecipe>(entity =>
            {
                entity.ToTable("UserRecipes");
                entity.HasKey(ur => new { ur.UserID, ur.RecipeID });

                entity.HasOne(ur => ur.Recipe)
                .WithMany(r => r.Likes)
                .HasForeignKey(umr => umr.RecipeID);

                entity.HasOne(ur => ur.User)
                .WithMany(u => u.LikedRecipes)
                .HasForeignKey(ur => ur.UserID);
            });
        }
    }
}
