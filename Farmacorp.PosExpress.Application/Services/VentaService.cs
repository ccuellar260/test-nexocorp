using System;
using Farmacorp.PosExpress.Application.DTOs;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;

namespace Farmacorp.PosExpress.Application.Services;

public class VentaService
{
    private readonly IUnitOfWork _unitOfWork;

    public VentaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<VentaDto>> Index()
    {
        var ventas = await _unitOfWork.VentaRepository.GetAllAsync();

        return ventas.Select(
            v => new VentaDto
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Cliente = v.Cliente,
                NombreProducto = v.NombreProducto,
                UniqueProducto = v.UniqueProducto,
                Cantidad = v.Cantidad,
                Precio = v.Precio,
                Descuento = v.Descuento,
                Total = v.Total
            }).ToList();
    }
    

     //store 
    public async Task<(bool Success, string ErrorMessage)> Store(CreateVentaDto dto)
    {
        try
        {
   
            //validaciones aqui
            if (dto.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero.");
            }


            var producto = await _unitOfWork.ErpProductoRepository.FirstByCodigoUniqueAsync(dto.CodigoProducto);

            if (producto == null)
            {
                throw new ArgumentException("El producto no existe.");
            }


            var venta = new Venta
            {
                Cliente = dto.Cliente,
                NombreProducto = producto.ExpProducto.Nombre,
                UniqueProducto = producto.UniqueCodigo,
                Cantidad = dto.Cantidad,
                Precio = producto.ExpProducto.Precio,
                Descuento = dto.Descuento,
                Total = producto.ExpProducto.Precio * dto.Cantidad - (dto.Descuento ?? 0),
                IdProducto = producto.IdProducto,
            };

            await _unitOfWork.VentaRepository.StoreAsync(venta);
            await _unitOfWork.SaveChangesAsync();

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, $"Error al registrar la venta: {ex.Message}");
        }
    }



}
