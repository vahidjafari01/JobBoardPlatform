using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoardPlatform.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Attachs_ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Attachs_LogoId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LogoId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InterviewAt",
                table: "Applications");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoId",
                table: "Companies",
                column: "LogoId",
                unique: true,
                filter: "[LogoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ResumeId",
                table: "AspNetUsers",
                column: "ResumeId",
                unique: true,
                filter: "[ResumeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Attachs_ResumeId",
                table: "AspNetUsers",
                column: "ResumeId",
                principalTable: "Attachs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Attachs_LogoId",
                table: "Companies",
                column: "LogoId",
                principalTable: "Attachs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Attachs_ResumeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Attachs_LogoId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LogoId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ResumeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePhotoId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewAt",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoId",
                table: "Companies",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePhotoId",
                table: "AspNetUsers",
                column: "ProfilePhotoId",
                unique: true,
                filter: "[ProfilePhotoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Attachs_ProfilePhotoId",
                table: "AspNetUsers",
                column: "ProfilePhotoId",
                principalTable: "Attachs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Attachs_LogoId",
                table: "Companies",
                column: "LogoId",
                principalTable: "Attachs",
                principalColumn: "Id");
        }
    }
}
