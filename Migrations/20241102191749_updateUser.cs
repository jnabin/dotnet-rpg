﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 251, 90, 129, 14, 73, 27, 83, 245, 77, 139, 162, 240, 187, 18, 182, 83, 8, 134, 247, 24, 77, 1, 51, 68, 235, 30, 218, 120, 227, 42, 224, 33, 251, 51, 86, 1, 75, 24, 255, 58, 148, 43, 245, 72, 109, 15, 210, 45, 168, 196, 107, 246, 119, 248, 154, 217, 225, 253, 170, 99, 171, 120, 148, 231 }, new byte[] { 196, 220, 247, 48, 152, 115, 7, 215, 189, 254, 29, 90, 232, 94, 140, 87, 158, 28, 160, 44, 84, 145, 170, 77, 83, 190, 162, 156, 65, 135, 122, 194, 119, 246, 50, 132, 143, 159, 202, 225, 156, 164, 76, 176, 242, 115, 209, 175, 207, 246, 209, 43, 163, 112, 162, 218, 168, 115, 139, 246, 117, 238, 144, 4, 28, 210, 227, 231, 77, 102, 79, 251, 60, 187, 146, 141, 5, 138, 225, 138, 112, 185, 8, 100, 74, 211, 118, 126, 61, 81, 41, 116, 41, 147, 191, 196, 251, 118, 214, 242, 249, 173, 148, 165, 198, 217, 38, 89, 194, 162, 218, 250, 196, 47, 52, 174, 86, 39, 220, 11, 221, 71, 107, 34, 138, 126, 119, 216 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 203, 232, 36, 130, 179, 171, 250, 247, 252, 253, 225, 175, 2, 42, 131, 24, 203, 149, 176, 151, 200, 196, 27, 80, 103, 71, 99, 88, 187, 197, 3, 228, 118, 44, 131, 46, 5, 196, 242, 164, 124, 8, 4, 98, 152, 169, 93, 129, 98, 235, 241, 213, 156, 16, 165, 101, 187, 128, 94, 119, 68, 245, 157, 67 }, new byte[] { 11, 23, 180, 86, 25, 107, 133, 41, 240, 246, 192, 203, 125, 43, 38, 12, 44, 3, 138, 88, 208, 240, 193, 130, 238, 11, 178, 55, 170, 4, 54, 75, 134, 232, 57, 72, 99, 253, 146, 109, 180, 67, 247, 131, 209, 94, 9, 229, 47, 80, 89, 226, 200, 78, 94, 46, 20, 193, 132, 198, 18, 194, 98, 56, 160, 197, 92, 188, 161, 198, 93, 119, 63, 73, 239, 107, 157, 149, 133, 104, 174, 213, 54, 251, 203, 227, 187, 147, 70, 10, 205, 136, 39, 194, 107, 165, 75, 145, 233, 197, 238, 16, 223, 198, 148, 139, 52, 2, 17, 90, 149, 215, 190, 20, 17, 254, 112, 131, 62, 109, 214, 79, 90, 102, 12, 16, 254, 62 } });
        }
    }
}
