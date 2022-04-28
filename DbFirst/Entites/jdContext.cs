using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DbFirst.Entites
{
    public partial class jdContext : DbContext
    {
        public jdContext()
        {
        }

        public jdContext(DbContextOptions<jdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<CityDict> CityDicts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.IdAuthor)
                    .HasName("Author_pk");

                entity.ToTable("Author");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("Book_pk");

                entity.ToTable("Book");

                entity.Property(e => e.PublishYear)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdAuthor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Book_Author");

                entity.HasOne(d => d.IdCityDictNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdCityDict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Book_CityDict");
            });

            modelBuilder.Entity<CityDict>(entity =>
            {
                entity.HasKey(e => e.IdCityDict)
                    .HasName("CityDict_pk");

                entity.ToTable("CityDict");

                entity.Property(e => e.IdCityDict).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
