using System;
using Farmacorp.PosExpress.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmacorp.PosExpress.Infrastructure.Persistence.Configurations;

public class ProductoCategoriaConfiguration : IEntityTypeConfiguration<ProductoCategoria>
{
    public void Configure(EntityTypeBuilder<ProductoCategoria> builder)
    {
        //nombre de la tabla 
        builder.ToTable("ProductosCategorias");

        //primary key
        builder.HasKey(pc => pc.IdDetalle);

        //auntoincremental
        builder.Property(pc => pc.IdDetalle)
               .ValueGeneratedOnAdd();

        //por defecto 
        builder.Property(pc => pc.FechaCreacion)
               .HasDefaultValueSql("GETDATE()");

        //relaciones con categoria belogn to, pertence a 
        builder.HasOne(pc => pc.Categoria)
               .WithMany(c => c.ProductosCategorias)
               .HasForeignKey(pc => pc.IdCategoria)
               .OnDelete(DeleteBehavior.Cascade);

        //relaciones con producto belogn to, pertence a 
        builder.HasOne(pc => pc.ExpProducto)
                .WithMany(p => p.ProductosCategorias)
                .HasForeignKey(pc => pc.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

    }

}
