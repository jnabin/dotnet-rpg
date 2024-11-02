using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    /// <inheritdoc />
    public partial class UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Role", "UserName" },
                values: new object[] { 12, new byte[] { 203, 232, 36, 130, 179, 171, 250, 247, 252, 253, 225, 175, 2, 42, 131, 24, 203, 149, 176, 151, 200, 196, 27, 80, 103, 71, 99, 88, 187, 197, 3, 228, 118, 44, 131, 46, 5, 196, 242, 164, 124, 8, 4, 98, 152, 169, 93, 129, 98, 235, 241, 213, 156, 16, 165, 101, 187, 128, 94, 119, 68, 245, 157, 67 }, new byte[] { 11, 23, 180, 86, 25, 107, 133, 41, 240, 246, 192, 203, 125, 43, 38, 12, 44, 3, 138, 88, 208, 240, 193, 130, 238, 11, 178, 55, 170, 4, 54, 75, 134, 232, 57, 72, 99, 253, 146, 109, 180, 67, 247, 131, 209, 94, 9, 229, 47, 80, 89, 226, 200, 78, 94, 46, 20, 193, 132, 198, 18, 194, 98, 56, 160, 197, 92, 188, 161, 198, 93, 119, 63, 73, 239, 107, 157, 149, 133, 104, 174, 213, 54, 251, 203, 227, 187, 147, 70, 10, 205, 136, 39, 194, 107, 165, 75, 145, 233, 197, 238, 16, 223, 198, 148, 139, 52, 2, 17, 90, 149, 215, 190, 20, 17, 254, 112, 131, 62, 109, 214, 79, 90, 102, 12, 16, 254, 62 }, "Admin", "AdminUser" });

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
