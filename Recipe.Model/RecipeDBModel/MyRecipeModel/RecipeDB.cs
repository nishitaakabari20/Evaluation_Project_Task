using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Test_api.Recipe.Model.MyRecipeModel;

public partial class RecipeDB : DbContext
{
    public RecipeDB(DbContextOptions<RecipeDB> options)
        : base(options)
    {
    }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipe__FDD988D00CC12CE0");

            entity.Property(e => e.CookingTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RecipeSid).HasDefaultValueSql("('Rec'+CONVERT([varchar](100),newid()))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
