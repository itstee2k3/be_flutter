using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace be_flutter_nhom2.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePost2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                schema: "identity",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "identity",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DateCrate",
                schema: "identity",
                table: "Posts",
                newName: "DateCreate");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                schema: "identity",
                table: "Posts",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                schema: "identity",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "identity",
                table: "Posts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateCreate",
                schema: "identity",
                table: "Posts",
                newName: "DateCrate");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                schema: "identity",
                table: "Posts",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
