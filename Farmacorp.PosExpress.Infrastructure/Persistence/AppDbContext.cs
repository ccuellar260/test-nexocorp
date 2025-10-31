using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Farmacorp.PosExpress.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ErpProducto> ErpProductos { get; set; }
        public DbSet<ExpProducto> ExpProductos { get; set; }
        public DbSet<CodigoBarra> CodigosBarras { get; set; }
        public DbSet<TipoProducto> TiposProductos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ProductoCategoria> ProductosCategorias { get; set; }
        public DbSet<Venta> Ventas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configuraciones fluent api
            modelBuilder.ApplyConfiguration(new ErpProductoConfiguration());
            modelBuilder.ApplyConfiguration(new ExpProductoConfiguration());
            modelBuilder.ApplyConfiguration(new CodigoBarraConfiguration());
            modelBuilder.ApplyConfiguration(new TipoProductoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new ProductoCategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new VentaConfiguration());
        }
    }
}
