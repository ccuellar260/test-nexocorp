using Farmacorp.PosExpress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmacorp.PosExpress.Infrastructure.Data.Configurations
{
    public class CodigoBarraConfiguration : IEntityTypeConfiguration<CodigoBarra>
    {
        public void Configure(EntityTypeBuilder<CodigoBarra> builder)
        {
            builder.ToTable("CodigosBarras");

            // 🔑 PK
            builder.HasKey(c => c.IdCodigoBarra);

            // ⚙️ Propiedades
            builder.Property(c => c.IdCodigoBarra)
                   .ValueGeneratedOnAdd(); // Identity auto-incremental

            builder.Property(c => c.UniqueCodigo)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(c => c.Activo)
                   .HasDefaultValue(true);

            // Relación BelongsTo con ErpProducto
            builder.HasOne(c => c.ErpProducto)
                   .WithMany(p => p.CodigosBarras) 
                   .HasForeignKey(c => c.IdProducto)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
