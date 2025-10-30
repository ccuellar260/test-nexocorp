using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmacorp.PosExpress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductosMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpProductos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpProductos", x => x.IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "ErpProductos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    UniqueCodigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Stock = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpProductos", x => x.IdProducto);
                    table.CheckConstraint("CK_ErpProductos_Costo", "[Costo] >= 0");
                    table.CheckConstraint("CK_ErpProductos_Stock", "[Stock] >= 0");
                    table.ForeignKey(
                        name: "FK_ErpProductos_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErpProductos_UniqueCodigo",
                table: "ErpProductos",
                column: "UniqueCodigo",
                unique: true,
                filter: "[UniqueCodigo] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErpProductos");

            migrationBuilder.DropTable(
                name: "ExpProductos");
        }
    }
}
