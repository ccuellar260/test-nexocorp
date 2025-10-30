using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Application.DTOs
{
    public class ProductoDto
    {
        public string UniqueCodigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // calculadas adicionales
        public string PrecioFormateado => $"${Precio:F2}";
        public string CostoFormateado => $"${Costo:F2}";
        public string EstadoStock => Stock > 0 ? "Disponible" : "Agotado";
    }
}
