
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

        public ErpProducto ErpProducto { get; set; } = null!;


    }
}