using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Viajes.Entities;

public partial class ViajesContext : DbContext
{
    private readonly IConfiguration _config;
    public ViajesContext(IConfiguration config)
    {
        _config = config;
    }

    public ViajesContext(DbContextOptions<ViajesContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<Viaje> Viajes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("server=DESKTOP-JMG5EHG;database=Viajes;Integrated Security=true;TrustServerCertificate=True");
        => optionsBuilder.UseSqlServer(_config.GetConnectionString("ConexionDb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__Ciudad__D4D3CCB0A8C6BF79");

            entity.ToTable("Ciudad");

            entity.Property(e => e.IdCiudad).ValueGeneratedNever();
            entity.Property(e => e.NombreCiudad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdTipoVehiculo).HasName("PK__TipoVehi__DC20741E7C157B4A");

            entity.ToTable("TipoVehiculo");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo).HasName("PK__Vehiculo__708612156673310A");

            entity.ToTable("Vehiculo");

            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patente)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoVehiculoNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.IdTipoVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculo__IdTipo__48CFD27E");
        });

        modelBuilder.Entity<Viaje>(entity =>
        {
            entity.HasKey(e => e.IdViaje).HasName("PK__Viaje__580AB6B92E82FBB7");

            entity.ToTable("Viaje");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdCiudadDesdeNavigation).WithMany(p => p.ViajeIdCiudadDesdeNavigations)
                .HasForeignKey(d => d.IdCiudadDesde)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viaje__IdCiudadD__534D60F1");

            entity.HasOne(d => d.IdCiudadHastaNavigation).WithMany(p => p.ViajeIdCiudadHastaNavigations)
                .HasForeignKey(d => d.IdCiudadHasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viaje__IdCiudadH__5441852A");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.IdVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viaje__IdVehicul__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
