using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JustBakery.Models
{
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.


  public class ApplicationUser : IdentityUser
  {
    public virtual Person Person { get; set; }
    public System.DateTime RegestrationDate { get; set; }
    public string Salt { get; set; }
  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
      : base("IdentityConnection")
    {
    }

    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //{
    //  base.OnModelCreating(modelBuilder);

    //  modelBuilder.Entity<IdentityUser>().ToTable("Аккаунты", "Class").Property(p => p.Id).HasColumnName("ID_Аккаунта");
    //  modelBuilder.Entity<IdentityUser>().ToTable("Аккаунты", "Class").Property(p => p.PasswordHash).HasColumnName("Пароль");
    //  modelBuilder.Entity<IdentityUser>().ToTable("Аккаунты", "Class").Property(p => p.UserName).HasColumnName("Логин");
    //  modelBuilder.Entity<ApplicationUser>().ToTable("Аккаунты", "Class").Property(p => p.Id).HasColumnName("ID_Аккаунта");
    //  modelBuilder.Entity<ApplicationUser>().ToTable("Аккаунты", "Class").Property(p => p.PasswordHash).HasColumnName("Пароль");
    //  modelBuilder.Entity<ApplicationUser>().ToTable("Аккаунты", "Class").Property(p => p.UserName).HasColumnName("Логин");
    //  modelBuilder.Entity<IdentityRole>().ToTable("Роли пользователей", "Class").Property(p => p.Id).HasColumnName("ID_Роли");
    //  modelBuilder.Entity<IdentityRole>().ToTable("Роли пользователей", "Class").Property(p => p.Name).HasColumnName("Роль");
    //  modelBuilder.Entity<IdentityUserRole>().ToTable("Аккаунты - Роли", "Class").Property(p => p.RoleId).HasColumnName("ID_Роли");
    //  modelBuilder.Entity<IdentityUserRole>().ToTable("Аккаунты - Роли", "Class").Property(p => p.UserId).HasColumnName("ID_Аккаунта");

    //}
  }
}