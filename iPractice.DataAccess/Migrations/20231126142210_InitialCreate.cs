using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPractice.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Psychologists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psychologists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PsychologistId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availabilities_Psychologists_PsychologistId",
                        column: x => x.PsychologistId,
                        principalTable: "Psychologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientDtoPsychologistDto",
                columns: table => new
                {
                    ClientsId = table.Column<long>(type: "INTEGER", nullable: false),
                    PsychologistsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDtoPsychologistDto", x => new { x.ClientsId, x.PsychologistsId });
                    table.ForeignKey(
                        name: "FK_ClientDtoPsychologistDto_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientDtoPsychologistDto_Psychologists_PsychologistsId",
                        column: x => x.PsychologistsId,
                        principalTable: "Psychologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientsPsychologists",
                columns: table => new
                {
                    ClientId = table.Column<long>(type: "INTEGER", nullable: false),
                    PsychologistId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ClientsPsychologists_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsPsychologists_Psychologists_PsychologistId",
                        column: x => x.PsychologistId,
                        principalTable: "Psychologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTimeFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateTimeTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClientId = table.Column<long>(type: "INTEGER", nullable: true),
                    AvailabilityId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Availabilities_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "Availabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_PsychologistId",
                table: "Availabilities",
                column: "PsychologistId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDtoPsychologistDto_PsychologistsId",
                table: "ClientDtoPsychologistDto",
                column: "PsychologistsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPsychologists_ClientId",
                table: "ClientsPsychologists",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPsychologists_PsychologistId",
                table: "ClientsPsychologists",
                column: "PsychologistId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_AvailabilityId",
                table: "TimeSlots",
                column: "AvailabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientDtoPsychologistDto");

            migrationBuilder.DropTable(
                name: "ClientsPsychologists");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "Psychologists");
        }
    }
}
