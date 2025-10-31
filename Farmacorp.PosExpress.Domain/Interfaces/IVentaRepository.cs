using System;
using Farmacorp.PosExpress.Domain.Entities;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface IVentaRepository
{
    Task<IEnumerable<Venta>> GetAllAsync();
    Task StoreAsync(Venta venta);
}
    