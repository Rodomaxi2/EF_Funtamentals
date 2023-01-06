using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Description", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("ed3c3bc7-7e9c-450f-b069-72dcd2712145"), null, "Escuela", 10 },
                    { new Guid("ed3c3bc7-7e9c-450f-b069-72dcd2712149"), null, "Terminar cursos", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Description", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("6661ba23-84e5-461c-aa2e-c5d12c97eaef"), new Guid("ed3c3bc7-7e9c-450f-b069-72dcd2712149"), null, new DateTime(2023, 1, 6, 16, 14, 11, 447, DateTimeKind.Local).AddTicks(8877), 1, "Terminar curso de EF" },
                    { new Guid("f6372f56-0b65-4550-8054-ec91070ba136"), new Guid("ed3c3bc7-7e9c-450f-b069-72dcd2712145"), null, new DateTime(2023, 1, 6, 16, 14, 11, 447, DateTimeKind.Local).AddTicks(8925), 2, "Recoger titulo fisico" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("6661ba23-84e5-461c-aa2e-c5d12c97eaef"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("f6372f56-0b65-4550-8054-ec91070ba136"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("ed3c3bc7-7e9c-450f-b069-72dcd2712145"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("ed3c3bc7-7e9c-450f-b069-72dcd2712149"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
