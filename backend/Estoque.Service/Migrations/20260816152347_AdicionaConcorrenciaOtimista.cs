using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estoque.Service.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaConcorrenciaOtimista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Produtos",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Produtos");
        }
    }
}
