using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plushing.Migrations
{
    /// <inheritdoc />
    public partial class Alteracaoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Cliente_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cor",
                columns: table => new
                {
                    CorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoHex = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cor", x => x.CorId);
                });

            migrationBuilder.CreateTable(
                name: "Pelucia",
                columns: table => new
                {
                    PeluciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelucia", x => x.PeluciaId);
                });

            migrationBuilder.CreateTable(
                name: "Carrinho",
                columns: table => new
                {
                    CarrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinho", x => x.CarrinhoId);
                    table.ForeignKey(
                        name: "FK_Carrinho_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_Venda_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personalizacao",
                columns: table => new
                {
                    PersonalizacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextoPersonalizado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalizacao", x => x.PersonalizacaoId);
                    table.ForeignKey(
                        name: "FK_Personalizacao_Cor_CorId",
                        column: x => x.CorId,
                        principalTable: "Cor",
                        principalColumn: "CorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roupa",
                columns: table => new
                {
                    RoupaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roupa", x => x.RoupaId);
                    table.ForeignKey(
                        name: "FK_Roupa_Cor_CorId",
                        column: x => x.CorId,
                        principalTable: "Cor",
                        principalColumn: "CorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeluciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeluciaId1 = table.Column<int>(type: "int", nullable: true),
                    PersonalizacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Carrinho_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinho",
                        principalColumn: "CarrinhoId");
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pelucia_PeluciaId1",
                        column: x => x.PeluciaId1,
                        principalTable: "Pelucia",
                        principalColumn: "PeluciaId");
                    table.ForeignKey(
                        name: "FK_ItemPedido_Personalizacao_PersonalizacaoId",
                        column: x => x.PersonalizacaoId,
                        principalTable: "Personalizacao",
                        principalColumn: "PersonalizacaoId");
                    table.ForeignKey(
                        name: "FK_ItemPedido_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "VendaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrinho_ClienteId",
                table: "Carrinho",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_UserId1",
                table: "Cliente",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_CarrinhoId",
                table: "ItemPedido",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PeluciaId1",
                table: "ItemPedido",
                column: "PeluciaId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PersonalizacaoId",
                table: "ItemPedido",
                column: "PersonalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_VendaId",
                table: "ItemPedido",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Personalizacao_CorId",
                table: "Personalizacao",
                column: "CorId");

            migrationBuilder.CreateIndex(
                name: "IX_Roupa_CorId",
                table: "Roupa",
                column: "CorId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_ClienteId",
                table: "Venda",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "Roupa");

            migrationBuilder.DropTable(
                name: "Carrinho");

            migrationBuilder.DropTable(
                name: "Pelucia");

            migrationBuilder.DropTable(
                name: "Personalizacao");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Cor");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
