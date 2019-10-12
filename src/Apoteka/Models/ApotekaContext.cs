using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Apoteka.Models
{
    public partial class ApotekaContext : DbContext
    {
        public ApotekaContext()
        {
        }

        public ApotekaContext(DbContextOptions<ApotekaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dobavljac> Dobavljac { get; set; }
        public virtual DbSet<Drzava> Drzava { get; set; }
        public virtual DbSet<Grad> Grad { get; set; }
        public virtual DbSet<Kupovina> Kupovina { get; set; }
        public virtual DbSet<Lijek> Lijek { get; set; }
        public virtual DbSet<Osiguranik> Osiguranik { get; set; }
        public virtual DbSet<Racun> Racun { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=apoteka");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Dobavljac>(entity =>
            {
                entity.ToTable("dobavljac", "apoteka");

                entity.HasIndex(e => e.GradId)
                    .HasName("fk_dobavljac_grad1_idx");

                entity.Property(e => e.DobavljacId).HasColumnType("int(11)");

                entity.Property(e => e.Adresa)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EMail)
                    .HasColumnName("E-mail")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.GradId).HasColumnType("int(11)");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Dobavljac)
                    .HasForeignKey(d => d.GradId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dobavljac_grad1");
            });

            modelBuilder.Entity<Drzava>(entity =>
            {
                entity.ToTable("drzava", "apoteka");

                entity.Property(e => e.DrzavaId).HasColumnType("int(11)");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grad>(entity =>
            {
                entity.ToTable("grad", "apoteka");

                entity.HasIndex(e => e.DrzavaId)
                    .HasName("fk_grad_drzava1_idx");

                entity.Property(e => e.GradId).HasColumnType("int(11)");

                entity.Property(e => e.DrzavaId).HasColumnType("int(11)");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PostanskiBroj)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Drzava)
                    .WithMany(p => p.Grad)
                    .HasForeignKey(d => d.DrzavaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_grad_drzava1");
            });

            modelBuilder.Entity<Kupovina>(entity =>
            {
                entity.HasKey(e => new { e.RacunId, e.LijekId });

                entity.ToTable("kupovina", "apoteka");

                entity.HasIndex(e => e.LijekId)
                    .HasName("fk_kupovina_lijek1_idx");

                entity.HasIndex(e => e.RacunId)
                    .HasName("fk_kupovina_racun1_idx");

                entity.Property(e => e.RacunId).HasColumnType("int(11)");

                entity.Property(e => e.LijekId).HasColumnType("int(11)");

                entity.Property(e => e.Iznos).HasColumnType("decimal(10,2)");

                entity.Property(e => e.Kolicina).HasColumnType("int(11)");

                entity.HasOne(d => d.Lijek)
                    .WithMany(p => p.Kupovina)
                    .HasForeignKey(d => d.LijekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_kupovina_lijek1");

                entity.HasOne(d => d.Racun)
                    .WithMany(p => p.Kupovina)
                    .HasForeignKey(d => d.RacunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_kupovina_racun1");
            });

            modelBuilder.Entity<Lijek>(entity =>
            {
                entity.ToTable("lijek", "apoteka");

                entity.HasIndex(e => e.DobavljacId)
                    .HasName("fk_lijek_dobavljac1_idx");

                entity.Property(e => e.LijekId).HasColumnType("int(11)");

                entity.Property(e => e.Cijena).HasColumnType("decimal(10,2)");

                entity.Property(e => e.DobavljacId).HasColumnType("int(11)");

                entity.Property(e => e.Kolicina).HasColumnType("int(11)");

                entity.Property(e => e.NaRecept).HasColumnType("tinyint(4)");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dobavljac)
                    .WithMany(p => p.Lijek)
                    .HasForeignKey(d => d.DobavljacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lijek_dobavljac1");
            });

            modelBuilder.Entity<Osiguranik>(entity =>
            {
                entity.ToTable("osiguranik", "apoteka");

                entity.HasIndex(e => e.GradId)
                    .HasName("fk_osiguranik_grad_idx");

                entity.Property(e => e.OsiguranikId).HasColumnType("int(11)");

                entity.Property(e => e.Adresa)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.BrojTelefona)
                    .HasColumnName("Broj telefona")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.GradId).HasColumnType("int(11)");

                entity.Property(e => e.Ime)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Jmbg)
                    .HasColumnName("JMBG")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Osiguranik)
                    .HasForeignKey(d => d.GradId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_osiguranik_grad");
            });

            modelBuilder.Entity<Racun>(entity =>
            {
                entity.ToTable("racun", "apoteka");

                entity.Property(e => e.RacunId).HasColumnType("int(11)");

                entity.Property(e => e.DatumIzdavanja).HasColumnType("date");

                entity.Property(e => e.Iznos).HasColumnType("decimal(10,2)");

                entity.Property(e => e.Jmbg)
                    .HasColumnName("JMBG")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });
        }
    }
}
