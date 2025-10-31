using System;
using Farmacorp.PosExpress.Application.DTOs;
using Farmacorp.PosExpress.Domain.Interfaces;

namespace Farmacorp.PosExpress.Application.Services;

public class TipoProductoService
{
    private readonly IUnitOfWork _unitOfWork;

    public TipoProductoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public IEnumerable<TipoProductoDto> Index()
    {
        var tipos = _unitOfWork.TipoProductoRepository.GetAll();

        return tipos.Select(
            tipo => new TipoProductoDto
            {
               IdTipoProducto = tipo.IdTipoProducto,
               Descripcion = tipo.Descripcion
            }).ToList();
    }

    //store 
    public async Task<(bool Success, string ErrorMessage)> Store(CreateTipoProductoDto dto)
    {
        try
        {
            var tipoProducto = new TipoProducto
            {
                Descripcion = dto.Descripcion
            };

            _unitOfWork.TipoProductoRepository.Store(tipoProducto);
            await _unitOfWork.SaveChangesAsync();

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, $"Error al registrar el tipo de producto: {ex.Message}");
        }
    }

}
