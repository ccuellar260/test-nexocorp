using System;

namespace Farmacorp.PosExpress.Application.DTOs;

public class CreateVentaDto
{
    public string? Cliente { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal? Descuento { get; set; }

    public string CodigoProducto { get; set; } = string.Empty;

}
