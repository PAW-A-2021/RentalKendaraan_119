using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Rental_Kendaraan_ItasContext : DbContext
    {
        public Rental_Kendaraan_ItasContext()
        {
        }

        public Rental_Kendaraan_ItasContext(DbContextOptions<Rental_Kendaraan_ItasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer1> Customer1s { get; set; }
        public virtual DbSet<Gender1> Gender1s { get; set; }
        public virtual DbSet<Jaminan1> Jaminan1s { get; set; }
        public virtual DbSet<JenisKendaraan1> JenisKendaraan1s { get; set; }
        public virtual DbSet<Kendaraan1> Kendaraan1s { get; set; }
        public virtual DbSet<KondisiKendaraan1> KondisiKendaraan1s { get; set; }
        public virtual DbSet<Peminjaman1> Peminjaman1s { get; set; }
        public virtual DbSet<Pengembalian1> Pengembalian1s { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer1>(entity =>
            {
                entity.HasKey(e => e.IdCustomer);

                entity.ToTable("Customer1");

                entity.Property(e => e.IdCustomer)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Customer");

                entity.Property(e => e.Alamat)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IdGender).HasColumnName("ID_Gender");

                entity.Property(e => e.NamaCustomer)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Customer");

                entity.Property(e => e.Nik)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("NIK");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("No_HP");

                entity.HasOne(d => d.IdGenderNavigation)
                    .WithMany(p => p.Customer1s)
                    .HasForeignKey(d => d.IdGender)
                    .HasConstraintName("FK_Customer1_Gender1");
            });

            modelBuilder.Entity<Gender1>(entity =>
            {
                entity.HasKey(e => e.IdGender);

                entity.ToTable("Gender1");

                entity.Property(e => e.IdGender)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Gender");

                entity.Property(e => e.NamaGender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Gender");
            });

            modelBuilder.Entity<Jaminan1>(entity =>
            {
                entity.HasKey(e => e.IdJaminan);

                entity.ToTable("Jaminan1");

                entity.Property(e => e.IdJaminan)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Jaminan");

                entity.Property(e => e.NamaJaminan)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Jaminan");
            });

            modelBuilder.Entity<JenisKendaraan1>(entity =>
            {
                entity.HasKey(e => e.IdJenisKendaraan);

                entity.ToTable("Jenis_Kendaraan1");

                entity.Property(e => e.IdJenisKendaraan)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Jenis_Kendaraan");

                entity.Property(e => e.NamaJenisKendaraan)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Jenis_Kendaraan");
            });

            modelBuilder.Entity<Kendaraan1>(entity =>
            {
                entity.HasKey(e => e.IdKendaraan);

                entity.ToTable("Kendaraan1");

                entity.Property(e => e.IdKendaraan)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Kendaraan");

                entity.Property(e => e.IdJenisKendaraan).HasColumnName("ID_Jenis_Kendaraan");

                entity.Property(e => e.Ketersediaan)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NamaKendaraan)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Kendaraan");

                entity.Property(e => e.NoPolisi)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("No_Polisi");

                entity.Property(e => e.NoStnk)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("No_STNK");

                entity.HasOne(d => d.IdJenisKendaraanNavigation)
                    .WithMany(p => p.Kendaraan1s)
                    .HasForeignKey(d => d.IdJenisKendaraan)
                    .HasConstraintName("FK_Kendaraan1_Jenis_Kendaraan1");
            });

            modelBuilder.Entity<KondisiKendaraan1>(entity =>
            {
                entity.HasKey(e => e.IdKondisi);

                entity.ToTable("Kondisi_Kendaraan1");

                entity.Property(e => e.IdKondisi)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Kondisi");

                entity.Property(e => e.NamaKondisi)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Kondisi");
            });

            modelBuilder.Entity<Peminjaman1>(entity =>
            {
                entity.HasKey(e => e.IdPeminjaman);

                entity.ToTable("Peminjaman1");

                entity.Property(e => e.IdPeminjaman)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Peminjaman");

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.IdJaminan).HasColumnName("ID_Jaminan");

                entity.Property(e => e.IdKendaraan).HasColumnName("ID_Kendaraan");

                entity.Property(e => e.TglPeminjaman)
                    .HasColumnType("datetime")
                    .HasColumnName("Tgl_Peminjaman");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Peminjaman1s)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_Peminjaman1_Customer1");

                entity.HasOne(d => d.IdJaminanNavigation)
                    .WithMany(p => p.Peminjaman1s)
                    .HasForeignKey(d => d.IdJaminan)
                    .HasConstraintName("FK_Peminjaman1_Jaminan1");

                entity.HasOne(d => d.IdKendaraanNavigation)
                    .WithMany(p => p.Peminjaman1s)
                    .HasForeignKey(d => d.IdKendaraan)
                    .HasConstraintName("FK_Peminjaman1_Kendaraan1");
            });

            modelBuilder.Entity<Pengembalian1>(entity =>
            {
                entity.HasKey(e => e.IdPengembalian);

                entity.ToTable("Pengembalian1");

                entity.Property(e => e.IdPengembalian)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Pengembalian");

                entity.Property(e => e.IdKondisi).HasColumnName("ID_Kondisi");

                entity.Property(e => e.IdPeminjaman).HasColumnName("ID_Peminjaman");

                entity.Property(e => e.TglPengembalian)
                    .HasColumnType("datetime")
                    .HasColumnName("Tgl_Pengembalian");

                entity.HasOne(d => d.IdKondisiNavigation)
                    .WithMany(p => p.Pengembalian1s)
                    .HasForeignKey(d => d.IdKondisi)
                    .HasConstraintName("FK_Pengembalian1_Kondisi_Kendaraan1");

                entity.HasOne(d => d.IdPeminjamanNavigation)
                    .WithMany(p => p.Pengembalian1s)
                    .HasForeignKey(d => d.IdPeminjaman)
                    .HasConstraintName("FK_Pengembalian1_Peminjaman1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
