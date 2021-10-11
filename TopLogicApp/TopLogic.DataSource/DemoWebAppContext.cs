using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TopLogic.DataSource
{
    public partial class DemoWebAppContext : DbContext
    {
        public DemoWebAppContext()
        {
        }

        public DemoWebAppContext(DbContextOptions<DemoWebAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"data source={ConfigurationManager.AppSettings[AppSettingConstants.DemoWebAppDatabaseConnection]}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName).HasMaxLength(30);

                entity.Property(e => e.Salary).HasColumnType("decimal(8, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
