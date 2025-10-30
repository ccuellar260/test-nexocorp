

namespace Farmacorp.PosExpress.Domain.Entities
{
    public class ErpProducto
    {
        public int IdProducto { get; set; }
        public decimal Costo { get; set; }
        public string UniqueCodigo { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public int Stock { get; set; }

        // relacion 
        public ExpProducto ExpProducto { get; set; } = null!;

        //relacion HasMany
        public ICollection<CodigoBarra> CodigosBarras { get; set; } = new List<CodigoBarra>();

        // Reglas de negocio
        public decimal CalcularMargenPrecio()
        {
            return Costo * 1.5m;
        }

        
    }
}