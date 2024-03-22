﻿namespace DataAccessLayer;

public class AppDbContext : IdentityDbContext<User>
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {
    Database.EnsureCreated();
  }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Furniture> Furnitures { get; set; }
  public DbSet<Image> Images { get; set; }
  public DbSet<Color> Colors { get; set; }
  public DbSet<OtpModel> OtpModels { get; set; }
  public DbSet<FurnitureColor> FurnitureColors { get; set; }
  public DbSet<Feedback> Feedbacks { get; set; }
  public DbSet<FeedbackBan> FeedbackBans { get; set; }

  //protected override void OnModelCreating(ModelBuilder builder)
  //{
  //  builder.Entity<Category>()
  //      .HasMany(c => c.Furnitures)
  //      .WithOne(f => f.Category)
  //      .HasForeignKey(f => f.CategoryId)
  //      .OnDelete(DeleteBehavior.Cascade);

  //  builder.Entity<Furniture>()
  //      .HasMany(f => f.Images)
  //      .WithOne(i => i.Furniture)
  //      .HasForeignKey(i => i.FurnitureId)
  //      .OnDelete(DeleteBehavior.Cascade);

  //  builder.Entity<FurnitureColor>()
  //         .HasKey(fc => new { fc.FurnitureId, fc.ColorId });

  //  builder.Entity<FurnitureColor>()
  //      .HasOne(fc => fc.Furniture)
  //      .WithMany(f => f.Colors)
  //      .HasForeignKey(fc => fc.FurnitureId);

  //  builder.Entity<FurnitureColor>()
  //      .HasOne(fc => fc.Color)
  //      .WithMany(c => c.Furnitures)
  //      .HasForeignKey(fc => fc.ColorId);

  //  builder.Entity<Furniture>()
  //      .HasMany(u => u.Feedbacks)
  //      .WithOne(f => f.Furniture)
  //      .HasForeignKey(f => f.FurnitureId)
  //      .OnDelete(DeleteBehavior.Cascade);

  //  builder.Entity<User>()
  //      .HasMany(u => u.Feedbacks)
  //      .WithOne(f => f.User)
  //      .HasForeignKey(f => f.UserId)
  //      .OnDelete(DeleteBehavior.Cascade);

  //  base.OnModelCreating(builder);
  //}
}