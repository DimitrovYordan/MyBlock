using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlock.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    TimePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorID = table.Column<int>(type: "int", nullable: true),
                    ParentPostID = table.Column<int>(type: "int", nullable: false),
                    ParentCommentID = table.Column<int>(type: "int", nullable: true),
                    TimePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentID",
                        column: x => x.ParentCommentID,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_ParentPostID",
                        column: x => x.ParentPostID,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DislikedByUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikedByUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DislikedByUsers_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DislikedByUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikedByUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedByUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedByUser_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedByUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "LastName", "Password", "Username" },
                values: new object[] { 1, "pesho@gmail.com", "Pesho", true, "Peshov", "pass123", "PeshoP" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "LastName", "Password", "Username" },
                values: new object[] { 2, "gosho@gmail.com", "Gosho", false, "Goshov", "pass123", "GoshoG" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorID", "Content", "Rating", "TimePosted", "Title" },
                values: new object[] { 1, 1, "Siguren li si?", 0, new DateTime(2023, 1, 14, 10, 45, 22, 526, DateTimeKind.Local).AddTicks(500), "Purvi post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorID", "Content", "Rating", "TimePosted", "Title" },
                values: new object[] { 2, 2, "Nqma da stane?", 0, new DateTime(2023, 1, 14, 10, 45, 22, 526, DateTimeKind.Local).AddTicks(1303), "Vtori post" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorID", "Content", "ParentCommentID", "ParentPostID", "TimePosted" },
                values: new object[] { 1, 1, "Tova e 1 komentar", null, 1, new DateTime(2023, 1, 14, 10, 45, 22, 522, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorID", "Content", "ParentCommentID", "ParentPostID", "TimePosted" },
                values: new object[] { 2, 2, "Tova e 2 komentar", null, 2, new DateTime(2023, 1, 14, 10, 45, 22, 525, DateTimeKind.Local).AddTicks(8105) });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorID",
                table: "Comments",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentID",
                table: "Comments",
                column: "ParentCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentPostID",
                table: "Comments",
                column: "ParentPostID");

            migrationBuilder.CreateIndex(
                name: "IX_DislikedByUsers_PostId",
                table: "DislikedByUsers",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_DislikedByUsers_UserId",
                table: "DislikedByUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedByUser_PostId",
                table: "LikedByUser",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedByUser_UserId",
                table: "LikedByUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorID",
                table: "Posts",
                column: "AuthorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DislikedByUsers");

            migrationBuilder.DropTable(
                name: "LikedByUser");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
