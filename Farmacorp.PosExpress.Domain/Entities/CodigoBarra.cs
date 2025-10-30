namespace Farmacorp.PosExpress.Domain.Entities
{
    public class CodigoBarra
    {
        public int IdCodigoBarra { get; set; }        // PK
        public string UniqueCodigo { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

        // Relacion, BelongsTo, le pertenece a uno ErpProducto
        public int IdProducto { get; set; }
        public ErpProducto ErpProducto { get; set; } = null!;
        
        //reglas de negocio
        public static string GenerarCodigo()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
        }
    }
}
