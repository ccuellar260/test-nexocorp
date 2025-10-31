
using Farmacorp.PosExpress.Domain.Interfaces;

namespace Farmacorp.PosExpress.Domain.Entities
{
    public class ExpProducto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; } = 0;
        public bool Activo { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string? Observaciones { get; set; }


        //relacioene uno a uno
        public ErpProducto ErpProducto { get; set; } = null!;

        //relacion pertences a uno 
        public int IdTipoProducto { get; set; }
        public TipoProducto TipoProducto { get; set; } = null!;


        //relaciones tiene muchos 
        public ICollection<ProductoCategoria> ProductosCategorias { get; set; } = new List<ProductoCategoria>();
        public ICollection<CodigoBarra> CodigosBarras { get; set; } = new List<CodigoBarra>();
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();


    }
}