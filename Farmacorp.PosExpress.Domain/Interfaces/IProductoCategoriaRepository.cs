using System;
using Farmacorp.PosExpress.Domain.Entities;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface IProductoCategoriaRepository
{
    IEnumerable<ProductoCategoria> GetAll();
    void Store(ProductoCategoria productoCategoria);
}
