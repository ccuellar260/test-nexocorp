using System;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Persistence;

namespace Farmacorp.PosExpress.Infrastructure.Repositories;

public class ProductoCategoriaRepository : IProductoCategoriaRepository
{
    private readonly AppDbContext _context;

    public ProductoCategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

     public IEnumerable<ProductoCategoria> GetAll()
    {
        return _context.ProductosCategorias.ToList();
    }

    public void Store(ProductoCategoria detalle)
    {
        _context.ProductosCategorias.Add(detalle);
    }
}