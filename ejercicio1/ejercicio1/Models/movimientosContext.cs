using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ejercicio1.Models
{
    public partial class movimientosContext : DbContext
    {
        public movimientosContext()
        {
        }

        public movimientosContext(DbContextOptions<movimientosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuenta> Cuenta { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=movimientos;User ID=sa;Password=context.123;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClieId)
                    .HasName("PK__CLIENTE__0C8BA50F606A4C40");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.ClieId).HasColumnName("CLIE_ID");

                entity.Property(e => e.ClieContrasenia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CLIE_CONTRASENIA");

                entity.Property(e => e.ClieEstado).HasColumnName("CLIE_ESTADO");

                entity.Property(e => e.PersId).HasColumnName("PERS_ID");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuenId)
                    .HasName("PK__CUENTA__6F3597748ED9DD15");

                entity.ToTable("CUENTA");

                entity.Property(e => e.CuenId).HasColumnName("CUEN_ID");

                entity.Property(e => e.ClieId).HasColumnName("CLIE_ID");

                entity.Property(e => e.CuenEstado).HasColumnName("CUEN_ESTADO");

                entity.Property(e => e.CuenNumero)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUEN_NUMERO");

                entity.Property(e => e.CuenSaldoInicial).HasColumnName("CUEN_SALDO_INICIAL");

                entity.Property(e => e.CuenTipo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUEN_TIPO");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.MoviId)
                    .HasName("PK__MOVIMIEN__79A706442C75EFD5");

                entity.ToTable("MOVIMIENTO");

                entity.Property(e => e.MoviId).HasColumnName("MOVI_ID");

                entity.Property(e => e.CuenId).HasColumnName("CUEN_ID");

                entity.Property(e => e.MoviFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("MOVI_FECHA");

                entity.Property(e => e.MoviSaldo).HasColumnName("MOVI_SALDO");

                entity.Property(e => e.MoviValor).HasColumnName("MOVI_VALOR");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.PersId)
                    .HasName("PK__PERSONA__7C6D1C6957074049");

                entity.ToTable("PERSONA");

                entity.Property(e => e.PersId).HasColumnName("PERS_ID");

                entity.Property(e => e.PersDireccion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PERS_DIRECCION");

                entity.Property(e => e.PersEdad).HasColumnName("PERS_EDAD");

                entity.Property(e => e.PersGenero)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PERS_GENERO");

                entity.Property(e => e.PersIdentificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PERS_IDENTIFICACION");

                entity.Property(e => e.PersNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERS_NOMBRE");

                entity.Property(e => e.PersTelefono)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PERS_TELEFONO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
