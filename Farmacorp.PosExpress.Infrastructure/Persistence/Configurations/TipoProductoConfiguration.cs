using System;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmacorp.PosExpress.Infrastructure.Persistence.Configurations;

public class TipoProductoConfiguration : IEntityTypeConfiguration<TipoProducto>
{
    public void Configure(EntityTypeBuilder<TipoProducto> builder)
    {
        //nombre de la tabla 
        builder.ToTable("TiposProductos");

        // primary key
        builder.HasKey(tp => tp.IdTipoProducto);

        // ajsute a la propuedades 
        builder.Property(tp => tp.IdTipoProducto)
               .ValueGeneratedOnAdd(); // Identity auto-incremental


        //requerido y maximo 100 caracteres
        builder.Property(tp => tp.Descripcion)
               .HasMaxLength(100)
               .IsRequired();


        //relaciones tiene mucho productos 
        builder.HasMany(tp => tp.ExpProductos)
               .WithOne(p => p.TipoProducto)
               .HasForeignKey(p => p.IdTipoProducto)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
