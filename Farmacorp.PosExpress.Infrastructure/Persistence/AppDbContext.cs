using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Farmacorp.PosExpress.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ErpProducto> ErpProductos { get; set; }
        public DbSet<ExpProducto> ExpProductos { get; set; }
        public DbSet<CodigoBarra> CodigosBarras { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configuraciones fluent api
            modelBuilder.ApplyConfiguration(new ErpProductoConfiguration());
            modelBuilder.ApplyConfiguration(new ExpProductoConfiguration());
            modelBuilder.ApplyConfiguration(new CodigoBarraConfiguration());
        }
    }
}
