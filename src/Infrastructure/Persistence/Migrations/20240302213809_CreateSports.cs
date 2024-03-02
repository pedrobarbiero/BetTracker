using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateSports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sports_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"),
                column: "ConcurrencyStamp",
                value: "5c8d1961-9731-4a12-b569-c7b9a5f506ac");

            migrationBuilder.InsertData(
                table: "Sports",
                columns: new[] { "Id", "ApplicationUserId", "CreatedDate", "Name", "Slug", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb001"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Football", "football", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb002"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cricket", "cricket", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb003"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Hockey", "hockey", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb004"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tennis", "tennis", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb005"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Volleyball", "volleyball", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb006"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Table Tennis", "table-tennis", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb007"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Basketball", "basketball", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb008"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Baseball", "baseball", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb009"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Rugby", "rugby", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a10af6b1-6994-4f31-8a81-4fe4e5efb010"), new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Golf", "golf", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sports_ApplicationUserId",
                table: "Sports",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: new Guid("c3a8e572-a950-4478-942f-2c2d5e38a001"),
                column: "ConcurrencyStamp",
                value: "ba317a96-106e-45f9-a189-e2920a2bf3a6");
        }
    }
}
