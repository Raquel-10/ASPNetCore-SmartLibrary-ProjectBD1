using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartLibrary.Entities;

namespace SmartLibrary.SqlDbContext
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autors { get; set; } = null!;
        public virtual DbSet<Autore> Autores { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Editorial> Editorials { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-SEECLQB\\SQLEXPRESS;Database=SmartLybrary;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("Autor", "Person");

                entity.HasIndex(e => e.PersonId, "UQ__Autor__AA2FFB84E3D4879E")
                    .IsUnique();

                entity.Property(e => e.AutorId)
                    .ValueGeneratedNever()
                    .HasColumnName("AutorID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Autors)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Autor__CountryID__3D5E1FD2");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Autor)
                    .HasForeignKey<Autor>(d => d.PersonId)
                    .HasConstraintName("FK__Autor__PersonID__3E52440B");
            });

            modelBuilder.Entity<Autore>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Lastname2).HasColumnName("lastname2");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books", "Book");

                entity.HasIndex(e => e.EditorialId, "UQ__Books__D54C828F6AF257FB")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .ValueGeneratedNever()
                    .HasColumnName("BookID");

                entity.Property(e => e.AutorId).HasColumnName("AutorID");

                entity.Property(e => e.EditorialId).HasColumnName("EditorialID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AutorId)
                    .HasConstraintName("FK__Books__AutorID__5BE2A6F2");

                entity.HasOne(d => d.Editorial)
                    .WithOne(p => p.Book)
                    .HasForeignKey<Book>(d => d.EditorialId)
                    .HasConstraintName("FK__Books__Editorial__5CD6CB2B");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__Books__GenderID__5AEE82B9");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente", "Person");

                entity.HasIndex(e => e.CountryId, "UQ__Cliente__10D160BE164AC664")
                    .IsUnique();

                entity.Property(e => e.ClienteId)
                    .ValueGeneratedNever()
                    .HasColumnName("ClienteID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Country)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey<Cliente>(d => d.CountryId)
                    .HasConstraintName("FK__Cliente__Country__44FF419A");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Cliente__PersonI__440B1D61");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "Person");

                entity.Property(e => e.CountryId)
                    .ValueGeneratedNever()
                    .HasColumnName("CountryID");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Iso).HasMaxLength(3);
            });

            modelBuilder.Entity<Editorial>(entity =>
            {
                entity.ToTable("Editorial", "Book");

                entity.HasIndex(e => e.CountryId, "UQ__Editoria__10D160BE7E2A15EB")
                    .IsUnique();

                entity.Property(e => e.EditorialId)
                    .ValueGeneratedNever()
                    .HasColumnName("EditorialID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.EditorialName).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Country)
                    .WithOne(p => p.Editorial)
                    .HasForeignKey<Editorial>(d => d.CountryId)
                    .HasConstraintName("FK__Editorial__Count__5629CD9C");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura", "Sales");

                entity.HasIndex(e => e.TipoDocumentId, "UQ__Factura__9274E7E5D2CB7CF0")
                    .IsUnique();

                entity.Property(e => e.FacturaId)
                    .ValueGeneratedNever()
                    .HasColumnName("FacturaID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.TipoDocumentId).HasColumnName("TipoDocumentID");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Factura__Cliente__4CA06362");

                entity.HasOne(d => d.TipoDocument)
                    .WithOne(p => p.Factura)
                    .HasForeignKey<Factura>(d => d.TipoDocumentId)
                    .HasConstraintName("FK__Factura__TipoDoc__4D94879B");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender", "Book");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("GenderID");

                entity.Property(e => e.GenderName).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.InvoiceDetailsId)
                    .HasName("PK__InvoiceD__9F18B3E530F82233");

                entity.ToTable("InvoiceDetails", "Sales");

                entity.Property(e => e.InvoiceDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("InvoiceDetailsID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.FacturaId).HasColumnName("FacturaID");

                entity.Property(e => e.Iva).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__InvoiceDe__BookI__60A75C0F");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.FacturaId)
                    .HasConstraintName("FK__InvoiceDe__Factu__619B8048");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person", "Person");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("PersonID");

                entity.Property(e => e.FirstName1).HasMaxLength(50);

                entity.Property(e => e.FirstName2).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName1).HasMaxLength(50);

                entity.Property(e => e.LastName2).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.TipoDocumentId)
                    .HasName("PK__TipoDocu__9274E7E4432DDCEF");

                entity.ToTable("TipoDocumento", "Sales");

                entity.Property(e => e.TipoDocumentId)
                    .ValueGeneratedNever()
                    .HasColumnName("TipoDocumentID");

                entity.Property(e => e.DocumentName).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
