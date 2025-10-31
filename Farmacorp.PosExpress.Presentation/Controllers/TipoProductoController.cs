using System;
using Farmacorp.PosExpress.Application.DTOs;
using Farmacorp.PosExpress.Application.Services;

namespace Farmacorp.PosExpress.Presentation.Controllers;

public class TipoProductoController
{
    private readonly TipoProductoService _tipoProductoService;

    public TipoProductoController(TipoProductoService tipoProductoService)
    {
        _tipoProductoService = tipoProductoService;
    }

    public async Task MenuTipoProductos()
    {
        while (true)
        {
            MostrarMenuTiposProductos();
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Index();
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



    private void MostrarMenuTiposProductos()
    {
        Console.Clear();
        Console.WriteLine("==== GESTION DE TIPOS DE PRODUCTOS ====");
        Console.WriteLine("1. Listar todos los Tipos de Productos");
        Console.WriteLine("2. Registrar Nuevo Tipo de Producto");
        Console.WriteLine("3. Volver al menu principal");
        Console.Write("Seleccione una opcion: ");
    }


    public void Index()
    {
        Console.Clear();
        Console.WriteLine("==== LISTA DE TIPOS DE PRODUCTOS ====");

        var tiposProductos =   _tipoProductoService.Index();

        if (!tiposProductos.Any())
        {
            Console.WriteLine("No hay tipos de productos registrados.");
        }
        else
        {

            Console.WriteLine($"{"Nro Tipo Producto",-15} {"Descripcion",-25}");
            Console.WriteLine(new string('=', 80));

            foreach (var tipoProducto in tiposProductos)
            {
                string DescripcionTruncado = tipoProducto.Descripcion.Length > 22
                    ? tipoProducto.Descripcion.Substring(0, 22) + "..."
                    : tipoProducto.Descripcion;

                Console.WriteLine($"{tipoProducto.IdTipoProducto,-15} " +
                                $"{DescripcionTruncado,-25} ");
            }
           
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }


       public async Task Create()
    {
        Console.Clear();
        Console.WriteLine("==== REGISTRO NUEVO TIPO DE PRODUCTO ====");
        Console.WriteLine();

        Console.Write("Descripcion: ");
        var descripcion = Console.ReadLine() ?? "";

        //validar que descripcion no este vacio
        if (string.IsNullOrWhiteSpace(descripcion))
        {
            Console.WriteLine("La descripcion no puede estar vacia.");
            Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
            Console.ReadKey();
            return;
        }

        var dto = new CreateTipoProductoDto
        {
            Descripcion = descripcion
        };

        var (success, errorMessage) = await _tipoProductoService.Store(dto);

        if (success)
        {
            Console.WriteLine("Tipo de producto registrado exitosamente!!!");
        }
        else
        {
            Console.WriteLine($"Error al registrar el tipo de producto: {errorMessage}");
        }

      

        Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
        Console.ReadKey();
    }

}