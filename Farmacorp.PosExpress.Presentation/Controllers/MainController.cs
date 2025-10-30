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

        public MainController(ProductoService productoService)
        {
            _productoService = productoService;
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
                        var productoController = new ProductoController(_productoService);
                        await productoController.MenuProductos();
                        break;
                    case "2":
                        // 
                        Console.WriteLine("desarrollo...");
                        Console.ReadKey();
                        break;
                    case "3":
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
            Console.WriteLine("2. Otras Gestiones");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
        }
    }
}
