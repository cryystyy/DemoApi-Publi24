using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class PrepopulationOfOneEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Exchange", "Isin", "Name", "Ticker", "Website" },
                values: new object[] { new Guid("39596349-95d5-4ae9-b9e9-aaffab046a64"), "Nasdaq", "US0378331005", "Apple Inc", "AAPL", "https://www.apple.com/" });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("071bc35b-6ec8-4cb0-aa1a-2f9c979fdd72"),
                column: "ConcurrencyStamp",
                value: "6470def5-42f9-4774-a759-501be7777ceb");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("dfa5a606-01f7-45bf-aea5-8b4d12f80b58"),
                column: "ConcurrencyStamp",
                value: "dbc34ecd-a099-419e-8d37-9390c2d5c27f");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b69809b9-3af8-463d-938c-aae2b8805939"),
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { true, "ad132a77-e8b9-4a51-995d-5e15dd55a0a5", "AQAAAAEAACcQAAAAEBGh9Vfjf98wR77IVwS3KlpFG4nLzx2s5AEQCo6BMAq0xpyIvbm6fOTYiBf9rKlkfw==", "832c63f1-488d-4f1a-bc4e-d9cfb215a24c" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b97e9fea-26ff-4214-8741-192428677bff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a59d091-cfcf-4b9e-889f-9793440ee0cf", "AQAAAAEAACcQAAAAEFqwXoclxmSiNjYeefro9U5e1axp/uU/TLhW/2b5GgdOiVTCFe1YgUyOOY00PW+oCA==", "b481e608-8838-49b5-bd94-c1b9f502a8e0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("39596349-95d5-4ae9-b9e9-aaffab046a64"));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("071bc35b-6ec8-4cb0-aa1a-2f9c979fdd72"),
                column: "ConcurrencyStamp",
                value: "e09ec5b5-1b49-4f5a-b13d-8e8229d90b95");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("dfa5a606-01f7-45bf-aea5-8b4d12f80b58"),
                column: "ConcurrencyStamp",
                value: "ad9e568b-2a4a-4f77-86ab-75b36ffb351c");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b69809b9-3af8-463d-938c-aae2b8805939"),
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { false, "1be59597-b0c0-4480-b04c-374b49211591", "AQAAAAEAACcQAAAAEAJMhH1Asf7+sQJ0yym7vL316R8kf80gZkWJDeUy+R3Y544/OdgE2pEfdbus5LTvrQ==", "f90219f8-32e7-4837-9698-c21ce76abaf4" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b97e9fea-26ff-4214-8741-192428677bff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a9ebef5-01a7-477b-a4c7-5f94b2564b19", "AQAAAAEAACcQAAAAEIbfGfD4o9Q5iY2r7fWX218lwHFEkBmZ3jdv06fnTK6Z+LabysPalYJYwxzBsp60vw==", "f14657e9-e187-4cbe-8672-b7b1ddfcadd2" });
        }
    }
}
