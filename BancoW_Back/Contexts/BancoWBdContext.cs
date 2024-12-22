using System;
using System.Collections.Generic;
using BancoW_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoW_Back.Contexts;

public partial class BancoWBdContext : DbContext
{
    public BancoWBdContext()
    {
    }

    public BancoWBdContext(DbContextOptions<BancoWBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Simulacion> Simulacions { get; set; }

    public virtual DbSet<TerminoPago> TerminoPagos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Simulacion>(entity =>
        {
            entity.HasKey(e => e.IdSimulacion).HasName("PK__Simulaci__97BA2A4B4850782F");

            entity.ToTable("Simulacion");

            entity.Property(e => e.IdSimulacion).HasColumnName("idSimulacion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.Tasa)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("tasa");
            entity.Property(e => e.TerminoPagoId).HasColumnName("termino_pago_id");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.TerminoPago).WithMany(p => p.Simulacions)
                .HasForeignKey(d => d.TerminoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Simulacion_TerminoPago");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Simulacions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Simulacion_Usuario");
        });

        modelBuilder.Entity<TerminoPago>(entity =>
        {
            entity.HasKey(e => e.IdTerminoDePago).HasName("PK__TerminoP__C6FB171B0F61F1C5");

            entity.ToTable("TerminoPago");

            entity.Property(e => e.IdTerminoDePago)
                .ValueGeneratedNever()
                .HasColumnName("idTerminoDePago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A67D3AF288");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__AB6E616414EB441D").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
