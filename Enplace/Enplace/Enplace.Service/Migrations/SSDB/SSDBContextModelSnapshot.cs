﻿// <auto-generated />
using System;
using Enplace.Service.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enplace.Service.Migrations.SSDB
{
    [DbContext(typeof(SSDBContext))]
    partial class SSDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Enplace.Service.Entities.DeltaStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Command")
                        .HasColumnType("int");

                    b.Property<int>("Target")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DeltaStore", (string)null);
                });

            modelBuilder.Entity("Enplace.Service.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IngredientCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("IngredientCategoryID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IngredientCategoryId");

                    b.HasIndex(new[] { "Name" }, "idx_Ingredients_NameAsc")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Enplace.Service.Entities.IngredientCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "idx_IngredientCategories_NameAsc")
                        .IsUnique();

                    b.ToTable("IngredientCategories");
                });

            modelBuilder.Entity("Enplace.Service.Entities.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "idx_Measurements_NameAsc")
                        .IsUnique();

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Enplace.Service.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApproximateCookingTime")
                        .HasColumnType("int");

                    b.Property<int?>("ApproximateServingSize")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("int")
                        .HasColumnName("OwnerUserID");

                    b.Property<int>("RecipeCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeCategoryID");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.HasIndex("RecipeCategoryId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RecipeCategories");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("MIME")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeImages");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int")
                        .HasColumnName("IngredientID");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<int>("MeasurementId")
                        .HasColumnType("int")
                        .HasColumnName("MeasurementID");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(10, 4)");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("MeasurementId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("Enplace.Service.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("SKID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SKID")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Enplace.Service.Entities.UserMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserMenus");
                });

            modelBuilder.Entity("Enplace.Service.Entities.UserMenuRecipe", b =>
                {
                    b.Property<int>("MenuID")
                        .HasColumnType("int");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<decimal>("Scale")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("MenuID", "RecipeID");

                    b.HasIndex("RecipeID");

                    b.ToTable("UserMenuRecipes", (string)null);
                });

            modelBuilder.Entity("Enplace.Service.Entities.UserRecipe", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "RecipeID");

                    b.HasIndex("RecipeID");

                    b.ToTable("UserRecipes", (string)null);
                });

            modelBuilder.Entity("Enplace.Service.Entities.Ingredient", b =>
                {
                    b.HasOne("Enplace.Service.Entities.IngredientCategory", "IngredientCategory")
                        .WithMany("Ingredients")
                        .HasForeignKey("IngredientCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_Ingredients_N1_IngredientCategories");

                    b.Navigation("IngredientCategory");
                });

            modelBuilder.Entity("Enplace.Service.Entities.Recipe", b =>
                {
                    b.HasOne("Enplace.Service.Entities.User", "OwnerUser")
                        .WithMany("OwnedRecipes")
                        .HasForeignKey("OwnerUserId")
                        .IsRequired()
                        .HasConstraintName("FK_Recipes_N1_Users");

                    b.HasOne("Enplace.Service.Entities.RecipeCategory", "Category")
                        .WithMany("Recipes")
                        .HasForeignKey("RecipeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_Recipes_N1_RecipeCategory");

                    b.Navigation("Category");

                    b.Navigation("OwnerUser");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeImage", b =>
                {
                    b.HasOne("Enplace.Service.Entities.Recipe", "Recipe")
                        .WithMany("RecipeImages")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_RecipeImages_N1_Recipe");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeIngredient", b =>
                {
                    b.HasOne("Enplace.Service.Entities.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_RecipeIngredients_N1_Ingredients");

                    b.HasOne("Enplace.Service.Entities.Measurement", "Measurement")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("MeasurementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_RecipeIngredients_N1_Measurements");

                    b.HasOne("Enplace.Service.Entities.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_RecipeIngredients_N1_Recipes");

                    b.Navigation("Ingredient");

                    b.Navigation("Measurement");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeStep", b =>
                {
                    b.HasOne("Enplace.Service.Entities.Recipe", "Recipe")
                        .WithMany("RecipeSteps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_RecipeSteps_N1_Recipes");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Enplace.Service.Entities.UserMenu", b =>
                {
                    b.HasOne("Enplace.Service.Entities.User", "User")
                        .WithMany("UserMenus")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("fk_UserMenus_N1_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enplace.Service.Entities.UserMenuRecipe", b =>
                {
                    b.HasOne("Enplace.Service.Entities.UserMenu", "Menu")
                        .WithMany("MenuRecipes")
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enplace.Service.Entities.Recipe", "Recipe")
                        .WithMany("MenuRecipes")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Enplace.Service.Entities.UserRecipe", b =>
                {
                    b.HasOne("Enplace.Service.Entities.Recipe", "Recipe")
                        .WithMany("Likes")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enplace.Service.Entities.User", "User")
                        .WithMany("LikedRecipes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enplace.Service.Entities.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("Enplace.Service.Entities.IngredientCategory", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Enplace.Service.Entities.Measurement", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("Enplace.Service.Entities.Recipe", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("MenuRecipes");

                    b.Navigation("RecipeImages");

                    b.Navigation("RecipeIngredients");

                    b.Navigation("RecipeSteps");
                });

            modelBuilder.Entity("Enplace.Service.Entities.RecipeCategory", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("Enplace.Service.Entities.User", b =>
                {
                    b.Navigation("LikedRecipes");

                    b.Navigation("OwnedRecipes");

                    b.Navigation("UserMenus");
                });

            modelBuilder.Entity("Enplace.Service.Entities.UserMenu", b =>
                {
                    b.Navigation("MenuRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
