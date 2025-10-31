
namespace Farmacorp.PosExpress.Domain.Entities;

public class ProductoCategoria
{
    public int IdDetalle { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    //realciones 
    public int IdProducto { get; set; }
    public ExpProducto ExpProducto { get; set; } = null!;

    public int IdCategoria { get; set; }
    public Categoria Categoria { get; set; } = null!;


}