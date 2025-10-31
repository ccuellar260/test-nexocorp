using Farmacorp.PosExpress.Domain.Entities;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public class TipoProducto
{
    public int IdTipoProducto { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    //relacion tiene muchos
    public ICollection<ExpProducto> ExpProductos { get; set; } = new List<ExpProducto>();
}