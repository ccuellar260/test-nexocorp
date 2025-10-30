using Farmacorp.PosExpress.Application.DTOs;
using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Domain.Interfaces;


namespace Farmacorp.PosExpress.Application.Services
{

    public class ProductoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductoDto> Index()
        {
            var productos = _unitOfWork.ErpProductoRepository.GetAll();
           
            return productos.Select(
                p => new ProductoDto
                {
                    UniqueCodigo = p.UniqueCodigo,
                    Nombre = p.ExpProducto.Nombre,
                    Costo = p.Costo,
                    Precio = p.ExpProducto.Precio,
                    Stock = p.Stock
                }).ToList();
        }

       
        public async Task<(bool Success, string ErrorMessage)> RegistrarProducto(CreateProductoDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // validaicones
                if (dto.Costo < 0)
                    return (false, "El costo no puede ser negativo");
                if (dto.Stock < 0)
                    return (false, "El stock no puede ser negativo");
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return (false, "El nombre del producto no puede estar vacoo.");

                
                var uniqueCodigo = _unitOfWork.ErpProductoRepository.GenerarCodigoUnico(dto.Nombre);

               
                var productosExistentes = _unitOfWork.ErpProductoRepository.GetAll();
                if (productosExistentes.Any(p => p.UniqueCodigo == uniqueCodigo))
                    return (false, "Ya existe un producto con el mismo código único.");


                // Crear ExpProducto
                var expProducto = new ExpProducto
                {
                    Nombre = dto.Nombre,
                    Observaciones = dto.Observaciones
                };

                // Crear ErpProducto y establecer la relación
                var erpProducto = new ErpProducto
                {
                    Costo = dto.Costo,
                    UniqueCodigo = uniqueCodigo,
                    FechaRegistro = DateTime.UtcNow,
                    Stock = dto.Stock,
                    ExpProducto = expProducto
                };

                // Calcular el precio usando la regla de negocio
                expProducto.Precio = erpProducto.CalcularMargenPrecio();

                // Guardar en los repositorios
                _unitOfWork.ExpProductoRepository.Store(expProducto);
                _unitOfWork.ErpProductoRepository.Store(erpProducto);

                // Guardar cambios en la base de datos
                await _unitOfWork.SaveChangesAsync();

                //verficar si se guardo correctamente

                //ya tengo el id del producto ERP generado
                int idProductoErp = erpProducto.IdProducto;
                    // Generar y guardar código de barra
                var codigoBarra = new CodigoBarra
                    {
                        UniqueCodigo = CodigoBarra.GenerarCodigo(),
                        Activo = true,
                        IdProducto = idProductoErp
                    };

                _unitOfWork.CodigoBarraRepository.Store(codigoBarra);

                int resultado = await _unitOfWork.SaveChangesAsync();
               

                await _unitOfWork.CommitTransactionAsync();
                return (resultado > 0, resultado > 0 ? string.Empty : "Error al guardar los cambios en la base de datos.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return (false, $"Error al registrar producto: {ex.Message}");
            }
        }
    }
}