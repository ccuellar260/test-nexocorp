using System;

namespace Farmacorp.PosExpress.Application.DTOs;

public class VentaDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string? Cliente { get; set; } = string.Empty;
    public string NombreProducto { get; set; } = string.Empty;
    public string UniqueProducto { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal? Descuento { get; set; }
    public decimal Total { get; set; }

}
