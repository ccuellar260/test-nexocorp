using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Application.DTOs
{
    public class CreateProductoDto
    {
        public string Nombre { get; set; } = "";
        public string Observaciones { get; set; } = "";
        public decimal Costo { get; set; }
        public int Stock { get; set; }
        public int TipoProductoId { get; set; }

    }
}
