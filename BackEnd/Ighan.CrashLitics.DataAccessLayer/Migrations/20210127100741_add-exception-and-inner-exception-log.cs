using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ighan.CrashLitics.DataAccessLayer.Migrations
{
    public partial class addexceptionandinnerexceptionlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExceptionLogs_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InnerExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExceptionLogId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    ExceptionLogId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InnerExceptionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InnerExceptionLogs_ExceptionLogs_ExceptionLogId1",
                        column: x => x.ExceptionLogId1,
                        principalTable: "ExceptionLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionLogs_DeviceId",
                table: "ExceptionLogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerExceptionLogs_ExceptionLogId1",
                table: "InnerExceptionLogs",
                column: "ExceptionLogId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InnerExceptionLogs");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");
        }
    }
}
