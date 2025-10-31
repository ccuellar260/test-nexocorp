using System;
using Farmacorp.PosExpress.Domain.Entities;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface ITipoProductoRepository
{
    IEnumerable<TipoProducto> GetAll();
    void Store(TipoProducto tipoProducto);
}
