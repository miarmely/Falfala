using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class change_column_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "MaritalStatuses",
                newName: "StatusName");

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
            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: (byte)4);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: (byte)5);

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "MaritalStatuses",
                newName: "Status");
        }
    }
}
