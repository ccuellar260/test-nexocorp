using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacorp.PosExpress.Application.Services;

namespace Farmacorp.PosExpress.Presentation.Controllers
{
    public class MainController
    {
        private readonly ProductoService _productoService;
        private readonly CategoriaService _categoriaServic;
        private readonly TipoProductoService _tipoProductoService;
        private readonly VentaService _ventaService;


        public MainController(ProductoService productoService, CategoriaService categoriaService, TipoProductoService tipoProductoService, VentaService ventaService)
        {
            _productoService = productoService;
            _categoriaServic = categoriaService;
            _tipoProductoService = tipoProductoService;
            _ventaService = ventaService;
        }

        public async Task Run()
        {

            while (true)
            {
                MostrarMenuPrincipal();
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        // Gestion de Productos
                        var productoController = new ProductoController(_productoService, _tipoProductoService);
                        await productoController.MenuProductos();
                        break;
                    case "2":
                        // Gestion de Tipos de Productos
                        var tipoProductoController = new TipoProductoController(_tipoProductoService);
                        await tipoProductoController.MenuTipoProductos();
                        break;
                    case "3":
                        // Gestion de Categorias
                        var categoriaController = new CategoriaController(_categoriaServic);
                        await categoriaController.MenuCategorias();
                        break;
                    case "4":
                        // Gestion de Ventas
                        var ventaController = new VentaController(_ventaService, _productoService);
                        await ventaController.MenuVentas();
                        break;
                    case "5":
                        Console.WriteLine("Hasta luego");
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            }

        }

        private void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("==== SISTEMA DE INVENTARIO ====");
            Console.WriteLine("1. Gestion de Productos");
            Console.WriteLine("2. Gestion de Tipos de Productos");
            Console.WriteLine("3. Gestion de Categorias");
            Console.WriteLine("4. Gestion de Ventas");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
        }
    }
}
