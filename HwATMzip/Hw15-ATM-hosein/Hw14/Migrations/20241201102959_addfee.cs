using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hw14.Migrations
{
    /// <inheritdoc />
    public partial class addfee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Fee",
                table: "Transactions",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Transactions");
        }
    }
}
