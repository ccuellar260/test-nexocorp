using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Infrastructure.Persistence;
using Farmacorp.PosExpress.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Infrastructure.Repositories;


public class ExpProductoRepository : IExpProductoRepository
{
    private readonly AppDbContext _context;

    public ExpProductoRepository(AppDbContext context)
    {
        _context = context;
    }


    public void Store(ExpProducto producto)
    {
        _context.ExpProductos.Add(producto);
        // return Save();
    }
    
    // public async Task<int> Save()
    // {
    //     return await _context.SaveChangesAsync();
    // }

}  
