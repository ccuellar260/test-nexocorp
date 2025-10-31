using Farmacorp.PosExpress.Application.Services;
using Farmacorp.PosExpress.Application.DTOs;
using System.Linq;
using System.Threading.Tasks;


namespace Farmacorp.PosExpress.Presentation.Controllers;

public class ProductoController
{
    private readonly ProductoService _productoService;
    private readonly TipoProductoService _tipoProductoService;

    public ProductoController(ProductoService productoService, TipoProductoService tipoProductoService)
    {
        _productoService = productoService;
        _tipoProductoService = tipoProductoService;
    }

    public async Task MenuProductos()
    {
        while (true)
        {
            MostrarMenuProductos();
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Index(); 
                    break;
                case "2":
                    await RegistrarProducto();
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

    private void MostrarMenuProductos()
    {
        Console.Clear();
        Console.WriteLine("==== GESTION DE PRODUCTOS ====");
        Console.WriteLine("1. Listar productos");
        Console.WriteLine("2. Registrar Nuevo Producto");
        Console.WriteLine("3. Volver al menu principal");
        Console.Write("Seleccione una opcion: ");
    }

    public void Index()
    {
        Console.Clear();
        Console.WriteLine("==== LISTA DE PRODUCTOS ====");

        var productos = _productoService.Index();

        if (!productos.Any())
        {
            Console.WriteLine("No hay productos registrados.");
        }
        else
        {
          
            Console.WriteLine($"{"Codigo Unico",-15} {"Nombre",-25} {"Costo",-12} {"Precio",-12} {"Stock",-8}");
            //80 veces = 
            Console.WriteLine(new string('=', 80));
            
            foreach (var producto in productos)
            {
                string nombreTruncado = producto.Nombre.Length > 22 
                    ? producto.Nombre.Substring(0, 22) + "..." 
                    : producto.Nombre;
                
                
                Console.WriteLine($"{(producto.UniqueCodigo ?? "N/A"),-15} " +
                                $"{nombreTruncado,-25} " +
                                $"{producto.CostoFormateado,-12} " +
                                $"{producto.PrecioFormateado,-12} " +
                                $"{producto.Stock,-8}");
            }
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    public async Task RegistrarProducto()
    {
        Console.Clear();
        Console.WriteLine("==== REGISTRO NUEVO PRODUCTO ====");
        Console.WriteLine();

        Console.Write("Nombre: ");
        var nombre = Console.ReadLine() ?? "";

        Console.Write("Observaciones: ");
        var observaciones = Console.ReadLine() ?? "";

        Console.Write("Costo: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal costo))
        {
            Console.WriteLine("error: El costo debe ser un numero decimal valido.");
            Console.ReadKey();
            return;
        }

        Console.Write("Stock: ");
        if (!int.TryParse(Console.ReadLine(), out int stock))
        {
            Console.WriteLine("error:  El stock debe ser un número entero válido.");
            Console.ReadKey();
            return;
        }


        // Mostrar lista de tipos de producto
        Console.WriteLine("Seleccione el tipo de producto:");

        var tiposProductos = _tipoProductoService.Index().ToList();
        var contador = 1;

        foreach (var tipo in tiposProductos)
        {
            // Mostramos cada tipo con un número consecutivo
            Console.WriteLine($"{contador}. {tipo.Descripcion}");
            contador++; 
        }

        // Pedir selección al usuario
        Console.Write("Seleccione una opcion: ");
        if (!int.TryParse(Console.ReadLine(), out int tipoSeleccionado) ||
            tipoSeleccionado < 1 ||
            tipoSeleccionado > tiposProductos.Count)
        {
            Console.WriteLine("opcion invalida.");
            Console.WriteLine("\nPresione cualquier tecla para ir al menu...");
            Console.ReadKey();
            return;
        }

        var tipoProducto = tiposProductos[tipoSeleccionado - 1];

        var dto = new CreateProductoDto
        {
            Nombre = nombre,
            Costo = costo,
            Stock = stock,
            Observaciones = observaciones,
            TipoProductoId = tipoProducto.IdTipoProducto
        };

        var (success, errorMessage) = await _productoService.RegistrarProducto(dto);

        if (success)
        {
            Console.WriteLine("Producto registrado exitosamente!!!");
        }
        else
        {
            Console.WriteLine($"Error al registrr producto: {errorMessage}");
        }

        Console.WriteLine("\nPresione cualquier tecla para ir al menú...");
        Console.ReadKey();
    }

}