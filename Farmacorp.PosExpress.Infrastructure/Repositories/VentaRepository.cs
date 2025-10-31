using System;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Farmacorp.PosExpress.Infrastructure.Repositories;

public class VentaRepository : IVentaRepository
{
    private readonly AppDbContext _context;

    public VentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Venta>> GetAllAsync()
    {
        return await _context.Ventas.ToListAsync();
    }

    public async Task StoreAsync(Venta venta)
    {
        await _context.Ventas.AddAsync(venta);
    }
}
