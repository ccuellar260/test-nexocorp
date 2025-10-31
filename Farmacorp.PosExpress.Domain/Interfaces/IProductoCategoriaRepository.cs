using System;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface IProductoCategoriaRepository
{
    IEnumerable<ProductoCategoria> GetAll();
    void Store(ProductoCategoria productoCategoria);
}
