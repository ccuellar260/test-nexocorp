using System;
using Farmacorp.PosExpress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Farmacorp.PosExpress.Infrastructure.Persistence.Configurations;

public class ExpProductoConfiguration : IEntityTypeConfiguration<ExpProducto>
{
    public void Configure(EntityTypeBuilder<ExpProducto> builder)
    {
        //nombre de la tabla 
        builder.ToTable("ExpProductos");

        // clave primaria
        builder.HasKey(p => p.IdProducto);

        //auto incremental
        builder.Property(p => p.IdProducto)
                .ValueGeneratedOnAdd();

        //ajustes a las propiedades
        builder.Property(p => p.Nombre)
            .HasColumnName("Nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Precio)
            .HasColumnName("Precio")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Activo)
            .HasColumnName("Activo")
            .HasDefaultValue(true);

        //not null , no es requerido
        builder.Property(p => p.FechaVencimiento)
            .HasColumnName("FechaVencimiento")
            .IsRequired(false);

        builder.Property(p => p.Observaciones)
            .HasColumnName("Observaciones")
            .HasMaxLength(250)
            .IsRequired(false);



        //relaciones,uno a uno erpproducto
        builder.HasOne(exp => exp.ErpProducto)
                .WithOne(erp => erp.ExpProducto)
                .HasForeignKey<ErpProducto>(erp => erp.IdProducto)
                .HasPrincipalKey<ExpProducto>(exp => exp.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

        //relacion con codigo de barram, has many 
        builder.HasMany(exp => exp.CodigosBarras)
               .WithOne(codigo => codigo.ExpProducto)
               .HasForeignKey(cb => cb.IdProducto)
               .OnDelete(DeleteBehavior.Cascade);

        //relaciones, belogn to pertenece a un tipo de producto
        builder.HasOne(exp => exp.TipoProducto)
               .WithMany(tipo => tipo.ExpProductos)
               .HasForeignKey(exp => exp.IdTipoProducto)
               .OnDelete(DeleteBehavior.Restrict);


    }
}

