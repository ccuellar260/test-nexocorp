using System;
using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Persistence;

namespace Farmacorp.PosExpress.Infrastructure.Repositories;

public class TipoProductoRepository : ITipoProductoRepository
{

    private readonly AppDbContext _context;


    public TipoProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TipoProducto> GetAll()
    {
        return _context.TiposProductos.ToList();
    }

    public void Store(TipoProducto tipoProducto)
    {
        _context.TiposProductos.Add(tipoProducto);
    }
}
