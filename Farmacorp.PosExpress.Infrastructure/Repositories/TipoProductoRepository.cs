using System;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
        return _context.TiposProductos
            .Include(tp => tp.ExpProductos)
            .ToList();
    }

    public void Store(TipoProducto tipoProducto)
    {
        _context.TiposProductos.Add(tipoProducto);
    }
}
