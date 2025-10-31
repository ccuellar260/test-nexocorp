using System;
using Farmacorp.PosExpress.Domain.Entities;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task StoreAsync(Categoria categoria);
}
