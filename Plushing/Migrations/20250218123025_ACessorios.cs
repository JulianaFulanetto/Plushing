using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plushing.Migrations
{
    /// <inheritdoc />
    public partial class ACessorios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AcessorioId",
                table: "Personalizacao",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Personalizacao_AcessorioId",
                table: "Personalizacao",
                column: "AcessorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personalizacao_Acessorio_AcessorioId",
                table: "Personalizacao",
                column: "AcessorioId",
                principalTable: "Acessorio",
                principalColumn: "AcessorioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personalizacao_Acessorio_AcessorioId",
                table: "Personalizacao");

            migrationBuilder.DropIndex(
                name: "IX_Personalizacao_AcessorioId",
                table: "Personalizacao");

            migrationBuilder.DropColumn(
                name: "AcessorioId",
                table: "Personalizacao");
        }
    }
}
