using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace be_flutter_nhom2.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                schema: "identity",
                table: "Posts",
                newName: "DateCrate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCrate",
                schema: "identity",
                table: "Posts",
                newName: "Data");
        }
    }
}
