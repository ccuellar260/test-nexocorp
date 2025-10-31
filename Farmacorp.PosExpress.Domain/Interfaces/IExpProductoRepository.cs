using Farmacorp.PosExpress.Domain.Entities;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface IExpProductoRepository
{
    void Store(ExpProducto producto);

    //buscar producto 
    Task<ExpProducto?> GetByIdAsync(int IdProducto);


    
}