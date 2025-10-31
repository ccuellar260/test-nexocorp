using Farmacorp.PosExpress.Domain.Entities;
using Farmacorp.PosExpress.Infrastructure.Persistence;
using Farmacorp.PosExpress.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacorp.PosExpress.Infrastructure.Repositories;


public class ErpProductoRepository : IErpProductoRepository
{
    private readonly AppDbContext _context;

    public ErpProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ErpProducto> GetAll()
    {
        var productos = _context.ErpProductos
            .Include(p => p.ExpProducto)
            .ToList();

        return productos;
    }

    public void Store(ErpProducto producto)
    {
        _context.ErpProductos.Add(producto);
        // return Save();
    }

    public string GenerarCodigoUnico(string nombre)
    {
        string codigoGenerado = GenerarCodigo(nombre);
        bool bandera = VerificarCodigoUnico(codigoGenerado);

        while (bandera)
        {
            codigoGenerado = GenerarCodigo(nombre);
            bandera = VerificarCodigoUnico(codigoGenerado);
        }

        return codigoGenerado;
    }

    private string GenerarCodigo(string nombre)
    {
        string iniciales;

        if (nombre.Length > 2)
        {
            iniciales = nombre.Substring(0, 3).ToUpper();
        }
        else
        {
            iniciales = nombre.ToUpper();
        }

        int numeroRando = new Random().Next(100, 999);

        return $"{iniciales}-{numeroRando}";
    }


    public bool VerificarCodigoUnico(string codigo)
    {
        var productosExistentes = _context.ErpProductos.ToList();
        return productosExistentes.Any(p => p.UniqueCodigo == codigo);
    }





    // public async Task<int> Save()
    // {
    //     return await _context.SaveChangesAsync();
    // }


}  
