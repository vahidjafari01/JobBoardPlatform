using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoardPlatform.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class edit33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Applications",
                newName: "NoteWritenByUser");

            migrationBuilder.AddColumn<Guid>(
                name: "ResumeId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NoteWritenByUser",
                table: "Applications",
                newName: "Note");
        }
    }
}
