using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FileItem> Files { get; set; }
        public DbSet<FileDirectory> Directories { get; set; }
        public DbSet<FileProperty> FileProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FileItem>(b =>
            {
                b.HasKey(it => it.Id);
                b.HasOne(it => it.Directory)
                 .WithMany(it => it.Files);

                b.HasOne(it => it.Property)
                 .WithOne();
                b.Property(it => it.Hash)
                 .HasColumnType("blob(384)");
            });


            builder.Entity<FileDirectory>()
                .HasOne(it => it.Parent)
                .WithMany(it => it.Children)
                .HasForeignKey(it => it.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}