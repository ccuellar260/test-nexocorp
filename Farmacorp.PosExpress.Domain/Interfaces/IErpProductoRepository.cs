using Farmacorp.PosExpress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Domain.Interfaces 
{ 

    public interface IErpProductoRepository
    {

        IEnumerable<ErpProducto> GetAll();

        void Store(ErpProducto producto);

        bool VerificarCodigoUnico(string codigo);

        string GenerarCodigoUnico(string nombre);

        // bool Save();
    }
}