using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSCajaAhorros.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContabilidadBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asientos_contables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_contable = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    monto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    tipo_operacion = table.Column<int>(type: "integer", nullable: false),
                    referencia_operacion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asientos_contables", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asientos_contables_estado",
                table: "asientos_contables",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "ix_asientos_contables_fecha_contable",
                table: "asientos_contables",
                column: "fecha_contable");

            migrationBuilder.CreateIndex(
                name: "ix_asientos_contables_referencia_operacion_id",
                table: "asientos_contables",
                column: "referencia_operacion_id");

            migrationBuilder.CreateIndex(
                name: "ix_asientos_contables_tipo_operacion",
                table: "asientos_contables",
                column: "tipo_operacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asientos_contables");
        }
    }
}
