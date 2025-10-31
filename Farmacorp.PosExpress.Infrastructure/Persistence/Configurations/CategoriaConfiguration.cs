using System;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmacorp.PosExpress.Infrastructure.Persistence.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
       public void Configure(EntityTypeBuilder<Categoria> builder)
       {
              //nombre de la tabla 
              builder.ToTable("Categorias");

              //primary key
              builder.HasKey(c => c.IdCategoria);

              //auntoincremental
              builder.Property(c => c.IdCategoria)
                     .ValueGeneratedOnAdd();

              //ajustes propiedades
              builder.Property(c => c.Descripcion)
                     .HasMaxLength(100)
                     .IsRequired();

              builder.Property(c => c.Activo)
                     .HasDefaultValue(true);

              //relaciones tiene muchos productocategoris
              builder.HasMany(categoria => categoria.ProductosCategorias)
                     .WithOne(productoCategoria => productoCategoria.Categoria)
                     .HasForeignKey(productoCategoria => productoCategoria.IdCategoria)
                     .OnDelete(DeleteBehavior.Restrict);

              //relacion asi mimsma categoria padre
              builder.HasOne(categoria => categoria.CategoriaPadre)
                     .WithMany()
                     .HasForeignKey(categoria => categoria.IdCategoriaPadre)
                     .OnDelete(DeleteBehavior.Restrict);

       }
}
