
namespace Farmacorp.PosExpress.Domain.Interfaces;

public class Categoria
{
    public int IdCategoria { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;

    //una categorias puede tener sub categorias
    public int? IdCategoriaPadre { get; set; }
    public Categoria? CategoriaPadre { get; set; }


    //relacion tiene muchos con productocategoria 
    public ICollection<ProductoCategoria> ProductosCategorias { get; set; } = new List<ProductoCategoria>();
}