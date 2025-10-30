
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Data;

namespace Farmacorp.PosExpress.Domain.Repositories;

public class CodigoBarraRepository : ICodigoBarraRepository
{
    private readonly AppDbContext _context;

    public CodigoBarraRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Store(CodigoBarra codigoBarra)
    {
        _context.CodigosBarras.Add(codigoBarra);
    }

    public bool VerificarCodigoUnico(string codigo)
    {
        return _context.CodigosBarras.Any(cb => cb.UniqueCodigo == codigo);
    }
}
