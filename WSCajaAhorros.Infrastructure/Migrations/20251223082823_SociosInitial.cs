using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSCajaAhorros.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SociosInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "socios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_persona = table.Column<int>(type: "integer", nullable: false),
                    tipo_identificacion = table.Column<int>(type: "integer", nullable: false),
                    numero_identificacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    nombres = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    apellidos = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    razon_social = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    nombre_comercial = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    fecha_constitucion = table.Column<DateOnly>(type: "date", nullable: true),
                    esta_activo = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "socios_correos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    socio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    correo_electronico = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    etiqueta = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    es_principal = table.Column<bool>(type: "boolean", nullable: false),
                    esta_activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socios_correos", x => x.id);
                    table.ForeignKey(
                        name: "fk_socios_correos_socios_socio_id",
                        column: x => x.socio_id,
                        principalTable: "socios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "socios_direcciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    socio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    direccion_linea1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    direccion_linea2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    direccion_ciudad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    direccion_provincia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    direccion_pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    direccion_referencia = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    etiqueta = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    es_principal = table.Column<bool>(type: "boolean", nullable: false),
                    esta_activa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socios_direcciones", x => x.id);
                    table.ForeignKey(
                        name: "fk_socios_direcciones_socios_socio_id",
                        column: x => x.socio_id,
                        principalTable: "socios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "socios_telefonos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    socio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    etiqueta = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    es_principal = table.Column<bool>(type: "boolean", nullable: false),
                    esta_activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socios_telefonos", x => x.id);
                    table.ForeignKey(
                        name: "fk_socios_telefonos_socios_socio_id",
                        column: x => x.socio_id,
                        principalTable: "socios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_socios_numero_identificacion",
                table: "socios",
                column: "numero_identificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_socios_correos_correo_electronico",
                table: "socios_correos",
                column: "correo_electronico");

            migrationBuilder.CreateIndex(
                name: "ix_socios_correos_socio_id",
                table: "socios_correos",
                column: "socio_id");

            migrationBuilder.CreateIndex(
                name: "ix_socios_direcciones_socio_id",
                table: "socios_direcciones",
                column: "socio_id");

            migrationBuilder.CreateIndex(
                name: "ix_socios_telefonos_socio_id_es_principal",
                table: "socios_telefonos",
                columns: new[] { "socio_id", "es_principal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "socios_correos");

            migrationBuilder.DropTable(
                name: "socios_direcciones");

            migrationBuilder.DropTable(
                name: "socios_telefonos");

            migrationBuilder.DropTable(
                name: "socios");
        }
    }
}
