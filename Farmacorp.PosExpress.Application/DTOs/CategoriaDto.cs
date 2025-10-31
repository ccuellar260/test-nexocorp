using System;

namespace Farmacorp.PosExpress.Application.DTOs;

public class CategoriaDto
{
    public int IdCategoria { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; }

    //calculos adicional 
    public string Estado => Activo ? "Activo" : "Inactivo";
}
