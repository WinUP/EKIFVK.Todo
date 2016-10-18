using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EKIFVK.Todo.API.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemPermission>(entity =>
            {
                entity.ToTable("System.Permission");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_System.Permission")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("System.User");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_System.User")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccessToken).HasColumnType("nchar(36)");

                entity.Property(e => e.LastAccessIp)
                    .HasColumnName("LastAccessIP")
                    .HasMaxLength(38);

                entity.Property(e => e.LastActiveTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("nchar(64)");

                entity.HasOne(d => d.UsergroupNavigation)
                    .WithMany(p => p.SystemUser)
                    .HasForeignKey(d => d.Usergroup)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_User_Usergroup");
            });

            modelBuilder.Entity<SystemUsergroup>(entity =>
            {
                entity.ToTable("System.Usergroup");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_System.Usergroup")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Enabled).HasDefaultValueSql("1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Finished).HasDefaultValueSql("0");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Task_System.User");
            });
        }

        public virtual DbSet<SystemPermission> SystemPermission { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<SystemUsergroup> SystemUsergroup { get; set; }
        public virtual DbSet<Task> Task { get; set; }
    }
}