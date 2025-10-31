using Farmacorp.PosExpress.Domain.Entities;


namespace Farmacorp.PosExpress.Domain.Interfaces;

public interface IErpProductoRepository
{

    IEnumerable<ErpProducto> GetAll();

    void Store(ErpProducto producto);

    bool VerificarCodigoUnico(string codigo);

    string GenerarCodigoUnico(string nombre);

    // bool Save();
}
