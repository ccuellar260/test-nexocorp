using System;
using Farmacorp.PosExpress.Application.DTOs;
using Farmacorp.PosExpress.Application.Services;

namespace Farmacorp.PosExpress.Presentation.Controllers;

public class CategoriaController
{
    private readonly CategoriaService _categoriaService;

    public CategoriaController(CategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    public async Task MenuCategorias()
    {
        while (true)
        {
            MostrarMenuCategorias();
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



    private void MostrarMenuCategorias()
    {
        Console.Clear();
        Console.WriteLine("==== GESTION DE CATEGORIAS ====");
        Console.WriteLine("1. Listar todas las Categorias");
        Console.WriteLine("2. Registrar Nuevo Categoria");
        Console.WriteLine("3. Volver al menu principal");
        Console.Write("Seleccione una opcion: ");
    }


    public async Task Index()
    {
        Console.Clear();
        Console.WriteLine("==== LISTA DE CATEGORIAS ====");

        var categorias =  await _categoriaService.Index();

        if (!categorias.Any())
        {
            Console.WriteLine("No hay categorias registradas.");
        }
        else
        {

            Console.WriteLine($"{"Nro Categoria",-15} {"Descripcion",-25} {"Estado",-12}");
            Console.WriteLine(new string('=', 80));

            foreach (var categoria in categorias)
            {
                string DescripcionTruncado = categoria.Descripcion.Length > 22
                    ? categoria.Descripcion.Substring(0, 22) + "..."
                    : categoria.Descripcion;

                Console.WriteLine($"{categoria.IdCategoria,-15} " +
                                $"{DescripcionTruncado,-25} " +
                                $"{categoria.Estado,-12} ");
            }
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }


       public async Task Create()
    {
        Console.Clear();
        Console.WriteLine("==== REGISTRO NUEVO CATEGORIA ====");
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

        var dto = new CreateCategoriaDto
        {
            Descripcion = descripcion
        };

        var (success, errorMessage) = await _categoriaService.Store(dto);

        if (success)
        {
            Console.WriteLine("Categoria registrada exitosamente!!!");
        }
        else
        {
            Console.WriteLine($"Error al registrar la categoria: {errorMessage}");
        }

      

        Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
        Console.ReadKey();
    }

}