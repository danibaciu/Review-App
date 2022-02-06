using System;
using Microsoft.EntityFrameworkCore;
using Models.Database.Entities;

namespace Models.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<Profile> profiles { get; set; }
        public DbSet<Preference> preferences { get; set; }

        public DbSet<Article> articles { get; set; }
        public DbSet<ArticleCategory> articleCategories { get; set; }
        public DbSet<Comment> comments { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #region Migrations
        //public DatabaseContext() { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.
        //        UseSqlServer(@"Server=localhost,1433;Database=ReviewAppCatalog;User=sa;Password=eLvisH@9699");
        //    }
        //}

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Role>().HasData(
            //     new Role[] {
            //     new Role{roleId=1, roleName="admin"},
            //     new Role{roleId=2, roleName="consumer"},
            //     new Role{roleId=3, roleName="creator"},
            // });

        }
    }
}
