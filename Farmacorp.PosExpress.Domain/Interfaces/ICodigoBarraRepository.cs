using Farmacorp.PosExpress.Domain.Entities;

namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface ICodigoBarraRepository
{

    void Store(CodigoBarra codigoBarra);

    bool VerificarCodigoUnico(string codigo);



}