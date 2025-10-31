using System;
using Farmacorp.PosExpress.Application.DTOs;
using Farmacorp.PosExpress.Application.Services;

namespace Farmacorp.PosExpress.Presentation.Controllers;

public class VentaController
{
    private readonly VentaService _ventaService;
    private readonly ProductoService _productoService;

    public VentaController(VentaService ventaService, ProductoService productoService)
    {
        _ventaService = ventaService;
        _productoService = productoService;
    }

    public async Task MenuVentas()
    {
        while (true)
        {
            MostrarMenuVentas();
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    await Index();
                    break;
                case "2":
                    await Create();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine(" Opcion invalida.");
                    Console.ReadKey();
                    break;
            }
        }
    }



    private void MostrarMenuVentas()
    {
        Console.Clear();
        Console.WriteLine("==== GESTION DE VENTAS ====");
        Console.WriteLine("1. Listar todas las ventas");
        Console.WriteLine("2. Registrar Nuevo venta");
        Console.WriteLine("3. Volver al menu principal");
        Console.Write("Seleccione una opcion: ");
    }


    public async Task Index()
    {
        Console.Clear();
        Console.WriteLine("==== LISTA DE VENTAS ====");

        var ventas =  await _ventaService.Index();

        if (!ventas.Any())
        {
            Console.WriteLine("No hay ventas registradas.");
        }
        else
        {

            Console.WriteLine($"{"Nro venta",-10} {"Fecha",-20} {"Cliente",-20} {"Producto",-20} {"UniqueProducto",-50} {"Cantidad",-10} {"Precio",-10} {"Descuento",-10} {"Total",-10}");
            Console.WriteLine(new string('=', 80));

            foreach (var venta in ventas)
            {
                string ProductoTruncado = venta.NombreProducto.Length > 22
                    ? venta.NombreProducto.Substring(0, 22) + "..."
                    : venta.NombreProducto;


                string ClienteTrucado;
                if (venta.Cliente == null)
                {
                    ClienteTrucado = "Desconocido";
                }
                else
                {
                    ClienteTrucado = venta.Cliente.Length > 17
                        ? venta.Cliente.Substring(0, 17) + "..."
                        : venta.Cliente;
                }

                Console.WriteLine($"{venta.Id,-10} " +
                                $"{venta.Fecha,-20} " +
                                $"{ClienteTrucado,-20} " +
                                $"{ProductoTruncado,-20} " +
                                $"{venta.UniqueProducto,-15} " +
                                $"{venta.Cantidad,-10} " +
                                $"{venta.Precio,-10} " +
                                $"{venta.Descuento,-10} " +
                                $"{venta.Total,-10} ");
            }
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }


       public async Task Create()
    {
        Console.Clear();
        Console.WriteLine("==== REGISTRO NUEVO VENTA ====");
        Console.WriteLine();

        Console.Write("Nombre del Cliente (opcional): ");
        var cliente = Console.ReadLine() ?? "";

        Console.Write("Cantidad: ");
        var cantidadInput = Console.ReadLine() ?? "";
        if (!int.TryParse(cantidadInput, out var cantidad))
        {
            Console.WriteLine("Cantidad invalida.");
            Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
            Console.ReadKey();
            return;
        }

        //Console.Write("Precio: ");
        //var precioInput = Console.ReadLine() ?? "";
        //if (!decimal.TryParse(precioInput, out var precio))
        //{
        //    Console.WriteLine("Precio invalido.");
        //    Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
        //    Console.ReadKey();
        //    return;
        //}

        Console.Write("Descuento (opcional): ");
        var descuentoInput = Console.ReadLine() ?? "";
        decimal? descuento = null;
        if (!string.IsNullOrWhiteSpace(descuentoInput))
        {
            if (decimal.TryParse(descuentoInput, out var descuentoValue))
            {
                descuento = descuentoValue;
            }
            else
            {
                Console.WriteLine("Descuento invalido.");
                Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
                Console.ReadKey();
                return;
            }
        }

        var codigoProducto = string.Empty;
        // var salir = false;
        // Elegir un producto
        do
        {
            Console.Write("Codigo de Producto: ");
            codigoProducto = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(codigoProducto))
            {
                Console.WriteLine("Codigo de producto invalido.");
                continue;
            }

            // Verificar si el producto existe
            var resultProducto = _productoService.ExistCodigoUnico(codigoProducto);

            if (resultProducto.Success)
            {
                break;
            }
            else
            {
                Console.WriteLine(resultProducto.ErrorMessage);
            }
            
        } while (true);

    


        var dto = new CreateVentaDto
        {
            Cliente = cliente,
            Cantidad = cantidad,
            Descuento = descuento,
            CodigoProducto = codigoProducto,
        };


        var (success, errorMessage) = await _ventaService.Store(dto);

        if (success)
        {
            Console.WriteLine("venta registrada exitosamente!!!");
        }
        else
        {
            Console.WriteLine($"Error al registrar la venta: {errorMessage}");
        }

      

        Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
        Console.ReadKey();
    }

}
