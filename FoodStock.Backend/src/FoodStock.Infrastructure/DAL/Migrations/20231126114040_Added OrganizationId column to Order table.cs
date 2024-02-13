using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStock.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrganizationIdcolumntoOrdertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrganizationId",
                table: "Orders",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Organizations_OrganizationId",
                table: "Orders",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Organizations_OrganizationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrganizationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Orders");
        }
    }
}
