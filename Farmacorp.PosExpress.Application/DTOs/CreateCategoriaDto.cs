using System;

namespace Farmacorp.PosExpress.Application.DTOs;

public class CreateCategoriaDto
{
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}
