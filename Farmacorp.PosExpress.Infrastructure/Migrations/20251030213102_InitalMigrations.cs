using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmacorp.PosExpress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodigosBarras_ErpProductos_IdProducto",
                table: "CodigosBarras");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoProducto",
                table: "ExpProductos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErpProductoIdProducto",
                table: "CodigosBarras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IdCategoriaPadre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_IdCategoriaPadre",
                        column: x => x.IdCategoriaPadre,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposProductos",
                columns: table => new
                {
                    IdTipoProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProductos", x => x.IdTipoProducto);
                });

            migrationBuilder.CreateTable(
                name: "ProductosCategorias",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCategorias", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpProductos_IdTipoProducto",
                table: "ExpProductos",
                column: "IdTipoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosBarras_ErpProductoIdProducto",
                table: "CodigosBarras",
                column: "ErpProductoIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_IdCategoriaPadre",
                table: "Categorias",
                column: "IdCategoriaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_IdCategoria",
                table: "ProductosCategorias",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_IdProducto",
                table: "ProductosCategorias",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosBarras_ErpProductos_ErpProductoIdProducto",
                table: "CodigosBarras",
                column: "ErpProductoIdProducto",
                principalTable: "ErpProductos",
                principalColumn: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosBarras_ExpProductos_IdProducto",
                table: "CodigosBarras",
                column: "IdProducto",
                principalTable: "ExpProductos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpProductos_TiposProductos_IdTipoProducto",
                table: "ExpProductos",
                column: "IdTipoProducto",
                principalTable: "TiposProductos",
                principalColumn: "IdTipoProducto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodigosBarras_ErpProductos_ErpProductoIdProducto",
                table: "CodigosBarras");

            migrationBuilder.DropForeignKey(
                name: "FK_CodigosBarras_ExpProductos_IdProducto",
                table: "CodigosBarras");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpProductos_TiposProductos_IdTipoProducto",
                table: "ExpProductos");

            migrationBuilder.DropTable(
                name: "ProductosCategorias");

            migrationBuilder.DropTable(
                name: "TiposProductos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_ExpProductos_IdTipoProducto",
                table: "ExpProductos");

            migrationBuilder.DropIndex(
                name: "IX_CodigosBarras_ErpProductoIdProducto",
                table: "CodigosBarras");

            migrationBuilder.DropColumn(
                name: "IdTipoProducto",
                table: "ExpProductos");

            migrationBuilder.DropColumn(
                name: "ErpProductoIdProducto",
                table: "CodigosBarras");

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosBarras_ErpProductos_IdProducto",
                table: "CodigosBarras",
                column: "IdProducto",
                principalTable: "ErpProductos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
