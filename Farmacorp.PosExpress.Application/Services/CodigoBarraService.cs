
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;

namespace Farmacorp.PosExpress.Domain.Aplication.Services;


public class CodigoBarraService
{
    private readonly IUnitOfWork _unitOfWork;

    public CodigoBarraService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }




    //guardar el codigo de barras 
    public async Task Store(int idProducto)
    {
        //llamra al metodo static 
        string nuevoCodigo;
        bool bandera;

        do
        {
            nuevoCodigo = CodigoBarra.GenerarCodigo();
            bandera = _unitOfWork.CodigoBarraRepository.VerificarCodigoUnico(nuevoCodigo);

        } while (bandera); 
        
        CodigoBarra codigoBarra = new CodigoBarra
        {
            UniqueCodigo = nuevoCodigo,
            IdProducto = idProducto,
            Activo = true
        };

        _unitOfWork.CodigoBarraRepository.Store(codigoBarra);
        await _unitOfWork.SaveChangesAsync();
    }



}