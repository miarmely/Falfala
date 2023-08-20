using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Falfala_MVC.Migrations
{
    /// <inheritdoc />
    public partial class Start_Point : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "varchar(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    TelNo = table.Column<string>(type: "char(11)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    Age = table.Column<byte>(type: "tinyint", nullable: false),
                    Job = table.Column<string>(type: "varchar(50)", nullable: true),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    StatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MaritalStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { (byte)1, "Bekar" },
                    { (byte)2, "Evli" },
                    { (byte)3, "İlişkisi var" },
                    { (byte)4, "İlişkisi yok" },
                    { (byte)5, "Karmaşık" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
