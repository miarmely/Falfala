using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_And_MaritalStatuses_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "varchar(12)", nullable: false)
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
                    FullName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    TelNo = table.Column<string>(type: "char(11)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Age = table.Column<byte>(type: "tinyint", nullable: false),
                    Job = table.Column<string>(type: "varchar(50)", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    StatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "mert123", "mostmert" },
                    { 2, "furkan123", "mostfurkan" },
                    { 3, "eda123", "mosteda" },
                    { 4, "erce123", "mosterce" }
                });
        }
    }
}
