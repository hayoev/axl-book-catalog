using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddInitProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastLogoutDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpireDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUsers_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminUsers_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdminRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminRoles_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminRoles_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fullname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeathDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authors_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PasswordSalt = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LastLoginDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdminRoleAdminPermissions",
                columns: table => new
                {
                    AdminRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminPermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRoleAdminPermissions", x => new { x.AdminRoleId, x.AdminPermissionId });
                    table.ForeignKey(
                        name: "FK_AdminRoleAdminPermissions_AdminPermissions_AdminPermissionId",
                        column: x => x.AdminPermissionId,
                        principalTable: "AdminPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminRoleAdminPermissions_AdminRoles_AdminRoleId",
                        column: x => x.AdminRoleId,
                        principalTable: "AdminRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminRoleAdminPermissions_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminRoleAdminPermissions_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdminUserAdminRoles",
                columns: table => new
                {
                    AdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUserAdminRoles", x => new { x.AdminUserId, x.AdminRoleId });
                    table.ForeignKey(
                        name: "FK_AdminUserAdminRoles_AdminRoles_AdminRoleId",
                        column: x => x.AdminRoleId,
                        principalTable: "AdminRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminUserAdminRoles_AdminUsers_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminUserAdminRoles_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminUserAdminRoles_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Cover = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublishYear = table.Column<short>(type: "smallint", nullable: false),
                    PageCount = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCategories_AdminUsers_CreatedByAdminUserId",
                        column: x => x.CreatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookCategories_AdminUsers_UpdatedByAdminUserId",
                        column: x => x.UpdatedByAdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBookshelfs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookshelfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBookshelfs_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBookshelfs_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBookshelfs_Users_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBookshelfs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "CreatedByAdminUserId", "CreatedDateTime", "Email", "FirstName", "IsActive", "LastLoginDateTime", "LastLogoutDateTime", "LastName", "MiddleName", "Password", "PasswordSalt", "RefreshToken", "RefreshTokenExpireDateTime", "UpdatedByAdminUserId", "UpdatedDateTime", "Username" },
                values: new object[] { new Guid("62010f17-95cd-11eb-9cfc-5254000c2f33"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "systemib@axl.com", "system", true, null, null, "system", null, "123", null, null, null, null, null, "system" });

            migrationBuilder.InsertData(
                table: "AdminRoles",
                columns: new[] { "Id", "Code", "CreatedByAdminUserId", "CreatedDateTime", "Description", "Name", "UpdatedByAdminUserId", "UpdatedDateTime" },
                values: new object[] { new Guid("e36f983b-95cd-11eb-9cfc-5254000c2f33"), "SADMIN", new Guid("62010f17-95cd-11eb-9cfc-5254000c2f33"), new DateTime(2022, 11, 11, 22, 26, 53, 116, DateTimeKind.Local).AddTicks(23), null, "Администратор", null, null });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "CreatedByAdminUserId", "CreatedDateTime", "Email", "FirstName", "IsActive", "LastLoginDateTime", "LastLogoutDateTime", "LastName", "MiddleName", "Password", "PasswordSalt", "RefreshToken", "RefreshTokenExpireDateTime", "UpdatedByAdminUserId", "UpdatedDateTime", "Username" },
                values: new object[] { new Guid("101fda50-9084-11eb-aef2-244bfee059a7"), new Guid("62010f17-95cd-11eb-9cfc-5254000c2f33"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@alx.com", "admin", true, null, null, "admin", null, "123", null, null, null, null, null, "admin" });

            migrationBuilder.InsertData(
                table: "AdminUserAdminRoles",
                columns: new[] { "AdminRoleId", "AdminUserId", "CreatedByAdminUserId", "CreatedDateTime", "Id", "UpdatedByAdminUserId", "UpdatedDateTime" },
                values: new object[] { new Guid("e36f983b-95cd-11eb-9cfc-5254000c2f33"), new Guid("101fda50-9084-11eb-aef2-244bfee059a7"), new Guid("62010f17-95cd-11eb-9cfc-5254000c2f33"), new DateTime(2022, 11, 11, 22, 26, 53, 122, DateTimeKind.Local).AddTicks(4285), new Guid("0e4ce9f0-95d1-11eb-9f9c-244bfee059a7"), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AdminPermissions_Code",
                table: "AdminPermissions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoleAdminPermissions_AdminPermissionId",
                table: "AdminRoleAdminPermissions",
                column: "AdminPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoleAdminPermissions_CreatedByAdminUserId",
                table: "AdminRoleAdminPermissions",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoleAdminPermissions_UpdatedByAdminUserId",
                table: "AdminRoleAdminPermissions",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoles_Code",
                table: "AdminRoles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoles_CreatedByAdminUserId",
                table: "AdminRoles",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoles_UpdatedByAdminUserId",
                table: "AdminRoles",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserAdminRoles_AdminRoleId",
                table: "AdminUserAdminRoles",
                column: "AdminRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserAdminRoles_CreatedByAdminUserId",
                table: "AdminUserAdminRoles",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserAdminRoles_UpdatedByAdminUserId",
                table: "AdminUserAdminRoles",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_CreatedByAdminUserId",
                table: "AdminUsers",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Email",
                table: "AdminUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_UpdatedByAdminUserId",
                table: "AdminUsers",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Username",
                table: "AdminUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedByAdminUserId",
                table: "Authors",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UpdatedByAdminUserId",
                table: "Authors",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BookId",
                table: "BookCategories",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CategoryId_BookId",
                table: "BookCategories",
                columns: new[] { "CategoryId", "BookId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CreatedByAdminUserId",
                table: "BookCategories",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_UpdatedByAdminUserId",
                table: "BookCategories",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedByAdminUserId",
                table: "Books",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UpdatedByAdminUserId",
                table: "Books",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedByAdminUserId",
                table: "Categories",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdatedByAdminUserId",
                table: "Categories",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookshelfs_BookId",
                table: "UserBookshelfs",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookshelfs_CreatedByUserId",
                table: "UserBookshelfs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookshelfs_UpdatedByUserId",
                table: "UserBookshelfs",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookshelfs_UserId_BookId",
                table: "UserBookshelfs",
                columns: new[] { "UserId", "BookId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedByAdminUserId",
                table: "Users",
                column: "CreatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedByAdminUserId",
                table: "Users",
                column: "UpdatedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRoleAdminPermissions");

            migrationBuilder.DropTable(
                name: "AdminUserAdminRoles");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "UserBookshelfs");

            migrationBuilder.DropTable(
                name: "AdminPermissions");

            migrationBuilder.DropTable(
                name: "AdminRoles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AdminUsers");
        }
    }
}
