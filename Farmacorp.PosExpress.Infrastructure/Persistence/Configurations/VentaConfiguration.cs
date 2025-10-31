using System;
using Farmacorp.PosExpress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmacorp.PosExpress.Infrastructure.Persistence.Configurations;

public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        //nomrbe de la tabla 
        builder.ToTable("VentaExpress");

        // primary key
        builder.HasKey(v => v.Id);

        //autoincremental 
        builder.Property(v => v.Id)
            .ValueGeneratedOnAdd();

        //fecha defauld 
        builder.Property(v => v.Fecha)
            .HasDefaultValueSql("GETDATE()");

        //puede ser nulo 
        builder.Property(v => v.Cliente)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(v => v.NombreProducto)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.UniqueProducto)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Cantidad)
            .IsRequired();

        builder.Property(v => v.Precio)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(v => v.Descuento)
            .HasColumnType("decimal(18,2)");

        builder.Property(v => v.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)");


        builder.HasOne(v => v.ExpProducto)
            .WithMany(exp => exp.Ventas)
            .HasForeignKey(v => v.IdProducto)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
