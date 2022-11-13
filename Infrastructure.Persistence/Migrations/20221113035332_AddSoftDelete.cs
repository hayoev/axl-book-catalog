using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeletedByUserId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Books",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedByUserId",
                table: "Authors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Authors",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AdminRoles",
                keyColumn: "Id",
                keyValue: new Guid("e36f983b-95cd-11eb-9cfc-5254000c2f33"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "AdminUserAdminRoles",
                keyColumns: new[] { "AdminRoleId", "AdminUserId" },
                keyValues: new object[] { new Guid("e36f983b-95cd-11eb-9cfc-5254000c2f33"), new Guid("101fda50-9084-11eb-aef2-244bfee059a7") },
                column: "CreatedDateTime",
                value: new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("101fda50-9084-11eb-aef2-244bfee059a7"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("62010f17-95cd-11eb-9cfc-5254000c2f33"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253));

            migrationBuilder.CreateIndex(
                name: "IX_Books_DeletedByUserId",
                table: "Books",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_DeletedByUserId",
                table: "Authors",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AdminUsers_DeletedByUserId",
                table: "Authors",
                column: "DeletedByUserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AdminUsers_DeletedByUserId",
                table: "Books",
                column: "DeletedByUserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AdminUsers_DeletedByUserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_AdminUsers_DeletedByUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_DeletedByUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_DeletedByUserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "AdminRoles",
                keyColumn: "Id",
                keyValue: new Guid("e36f983b-95cd-11eb-9cfc-5254000c2f33"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 11, 11, 22, 26, 53, 116, DateTimeKind.Local).AddTicks(23));

            migrationBuilder.UpdateData(
                table: "AdminUserAdminRoles",
                keyColumns: new[] { "AdminRoleId", "AdminUserId" },
                keyValues: new object[] { new Guid("e36f983b-95cd-11eb-9cfc-5254000c2f33"), new Guid("101fda50-9084-11eb-aef2-244bfee059a7") },
                column: "CreatedDateTime",
                value: new DateTime(2022, 11, 11, 22, 26, 53, 122, DateTimeKind.Local).AddTicks(4285));

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("101fda50-9084-11eb-aef2-244bfee059a7"),
                column: "CreatedDateTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("62010f17-95cd-11eb-9cfc-5254000c2f33"),
                column: "CreatedDateTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
