using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSCajaAhorros.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OperacionesInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movimientos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cuenta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    monto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    fecha_operacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    canal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    direccion_ip = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    transferencia_id = table.Column<Guid>(type: "uuid", nullable: true),
                    asiento_contable_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movimientos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transferencias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cuenta_origen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cuenta_destino_id = table.Column<Guid>(type: "uuid", nullable: false),
                    monto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_operacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    canal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    direccion_ip = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    observacion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    codigo_operacion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transferencias", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_movimientos_cuenta_id",
                table: "movimientos",
                column: "cuenta_id");

            migrationBuilder.CreateIndex(
                name: "ix_movimientos_fecha_operacion",
                table: "movimientos",
                column: "fecha_operacion");

            migrationBuilder.CreateIndex(
                name: "ix_movimientos_transferencia_id",
                table: "movimientos",
                column: "transferencia_id");

            migrationBuilder.CreateIndex(
                name: "ix_transferencias_codigo_operacion",
                table: "transferencias",
                column: "codigo_operacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transferencias_cuenta_destino_id",
                table: "transferencias",
                column: "cuenta_destino_id");

            migrationBuilder.CreateIndex(
                name: "ix_transferencias_cuenta_origen_id",
                table: "transferencias",
                column: "cuenta_origen_id");

            migrationBuilder.CreateIndex(
                name: "ix_transferencias_fecha_operacion",
                table: "transferencias",
                column: "fecha_operacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimientos");

            migrationBuilder.DropTable(
                name: "transferencias");
        }
    }
}
