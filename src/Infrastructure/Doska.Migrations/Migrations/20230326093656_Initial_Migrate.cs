using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doska.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_User_UserId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAd_Ad_AdId",
                table: "FavoriteAd");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteAd_AdId",
                table: "FavoriteAd");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "FavoriteAd");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Ad",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteAdId",
                table: "Ad",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ad_FavoriteAdId",
                table: "Ad",
                column: "FavoriteAdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_FavoriteAd_FavoriteAdId",
                table: "Ad",
                column: "FavoriteAdId",
                principalTable: "FavoriteAd",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_User_UserId",
                table: "Ad",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_FavoriteAd_FavoriteAdId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_Ad_User_UserId",
                table: "Ad");

            migrationBuilder.DropIndex(
                name: "IX_Ad_FavoriteAdId",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "FavoriteAdId",
                table: "Ad");

            migrationBuilder.AddColumn<Guid>(
                name: "AdId",
                table: "FavoriteAd",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Ad",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAd_AdId",
                table: "FavoriteAd",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_User_UserId",
                table: "Ad",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAd_Ad_AdId",
                table: "FavoriteAd",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
