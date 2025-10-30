using Farmacorp.PosExpress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Infrastructure.Data.Configurations
{
    public class ExpProductoConfiguration : IEntityTypeConfiguration<ExpProducto>
    {
        public void Configure(EntityTypeBuilder<ExpProducto> builder) 
        {

            builder.ToTable("ExpProductos");

            // clave primaria
            builder.HasKey(p => p.IdProducto);

            //auto incremental
            builder.Property(p => p.IdProducto)
                   .ValueGeneratedOnAdd();

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

            builder.Property(p => p.FechaVencimiento)
                .HasColumnName("FechaVencimiento")
                .IsRequired(false);

            builder.Property(p => p.Observaciones)
                .HasColumnName("Observaciones")
                .HasMaxLength(250)
                .IsRequired(false);
            
        
        }
    }
}
