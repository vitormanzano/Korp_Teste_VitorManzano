using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Faturamento.Service.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaNotaFiscal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotasFiscais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasFiscais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensNotaFiscal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotaFiscalId = table.Column<int>(type: "integer", nullable: false),
                    CodigoProduto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DescricaoProduto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensNotaFiscal", x => x.Id);
                    table.CheckConstraint("CK_ItemNotaFiscal_Quantidade_Positiva", "\"Quantidade\" > 0");
                    table.ForeignKey(
                        name: "FK_ItensNotaFiscal_NotasFiscais_NotaFiscalId",
                        column: x => x.NotaFiscalId,
                        principalTable: "NotasFiscais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensNotaFiscal_NotaFiscalId",
                table: "ItensNotaFiscal",
                column: "NotaFiscalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensNotaFiscal");

            migrationBuilder.DropTable(
                name: "NotasFiscais");
        }
    }
}
