using System;
using Farmacorp.PosExpress.Application.DTOs;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;

namespace Farmacorp.PosExpress.Application.Services;

public class CategoriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<CategoriaDto>> Index()
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetAllAsync();

        return categorias.Select(
            c => new CategoriaDto
            {
                IdCategoria = c.IdCategoria,
                Descripcion = c.Descripcion,
                Activo = c.Activo
            }).ToList();
    }

    //store 
    public async Task<(bool Success, string ErrorMessage)> Store(CreateCategoriaDto dto)
    {
        try
        {
            var categoria = new Categoria
            {
                Descripcion = dto.Descripcion,
                Activo = dto.Activo
            };

            await _unitOfWork.CategoriaRepository.StoreAsync(categoria);
            await _unitOfWork.SaveChangesAsync();

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, $"Error al registrar la categoría: {ex.Message}");
        }
    }

}
