using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_UserEntityId",
                table: "Cities",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Users_UserEntityId",
                table: "Cities",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Users_UserEntityId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_UserEntityId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Cities");
        }
    }
}
