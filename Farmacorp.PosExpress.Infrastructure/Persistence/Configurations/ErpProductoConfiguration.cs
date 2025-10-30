using Farmacorp.PosExpress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Farmacorp.PosExpress.Infrastructure.Data.Configurations
{
    public class ErpProductoConfiguration : IEntityTypeConfiguration<ErpProducto>
    {
        public void Configure(EntityTypeBuilder<ErpProducto> builder) 
        {
        
            builder.ToTable("ErpProductos");

            // clave primaria
            builder.HasKey(p => p.IdProducto);

            // no incremental, misma id de ExpProducto
            builder.Property(e => e.IdProducto)
                   .ValueGeneratedNever();

            
            builder.Property(p => p.UniqueCodigo)
                .HasMaxLength(20)
                .IsRequired(false); 

           
            builder.Property(p => p.Costo)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0.00m);

        
            builder.Property(p => p.FechaRegistro)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.Stock)
                .HasColumnName("Stock")
                .IsRequired()
                .HasDefaultValue(0);

            // indices
            builder.HasIndex(p => p.UniqueCodigo)
                .HasDatabaseName("IX_ErpProductos_UniqueCodigo")
                .IsUnique();

            // costo y stock mayores a 0
            builder.ToTable("ErpProductos", t =>
            {
                t.HasCheckConstraint("CK_ErpProductos_Stock", "[Stock] >= 0");
                t.HasCheckConstraint("CK_ErpProductos_Costo", "[Costo] >= 0");
            });

            //relaciones
            builder.HasOne(e => e.ExpProducto)
                   .WithOne(p => p.ErpProducto)
                   .HasForeignKey<ErpProducto>(e => e.IdProducto)
                   .HasPrincipalKey<ExpProducto>(p => p.IdProducto)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
