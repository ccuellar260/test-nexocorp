using Farmacorp.PosExpress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmacorp.PosExpress.Infrastructure.Persistence.Configurations;

public class CodigoBarraConfiguration : IEntityTypeConfiguration<CodigoBarra>
{
       public void Configure(EntityTypeBuilder<CodigoBarra> builder)
       {
              //nomrbe de la tabla 
              builder.ToTable("CodigosBarras");

              //primary key 
              builder.HasKey(c => c.IdCodigoBarra);

              //auto incremental
              builder.Property(c => c.IdCodigoBarra)
                     .ValueGeneratedOnAdd();

              builder.Property(c => c.UniqueCodigo)
                     .HasMaxLength(50)
                     .IsRequired();

              builder.Property(c => c.Activo)
                     .HasDefaultValue(true);

              // Relación BelongsTo con ExpProducto
              builder.HasOne(codigoBarra => codigoBarra.ExpProducto)
                     .WithMany(expProducto => expProducto.CodigosBarras)
                     .HasForeignKey(codigoBarra => codigoBarra.IdProducto)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}

