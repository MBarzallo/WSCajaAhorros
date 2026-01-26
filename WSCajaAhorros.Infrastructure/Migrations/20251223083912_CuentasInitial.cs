using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSCajaAhorros.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CuentasInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cuentas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    numero_cuenta = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    socio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    producto_cuenta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    saldo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    estado = table.Column<int>(type: "integer", nullable: false),
                    fecha_apertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cuentas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productos_cuenta",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    tasa_interes = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    permite_retiros = table.Column<bool>(type: "boolean", nullable: false),
                    permite_transferencias = table.Column<bool>(type: "boolean", nullable: false),
                    saldo_minimo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productos_cuenta", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cuentas_numero_cuenta",
                table: "cuentas",
                column: "numero_cuenta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_productos_cuenta_codigo",
                table: "productos_cuenta",
                column: "codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cuentas");

            migrationBuilder.DropTable(
                name: "productos_cuenta");
        }
    }
}
