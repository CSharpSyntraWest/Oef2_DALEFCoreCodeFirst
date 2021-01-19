using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.DataLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persoon",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Familienaam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 1, 19, 0, 0, 0, 0, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persoon", x => x.PersoonId);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "Vak",
                columns: table => new
                {
                    VakId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AantalLesuren = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vak", x => x.VakId);
                });

            migrationBuilder.CreateTable(
                name: "Docent",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", nullable: false),
                    Uurloon = table.Column<decimal>(type: "Decimal(10,2)", precision: 2, scale: 2, nullable: false),
                    DbSchoolSchoolId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docent", x => x.PersoonId);
                    table.ForeignKey(
                        name: "FK_Docent_Persoon_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Persoon",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Docent_School_DbSchoolSchoolId",
                        column: x => x.DbSchoolSchoolId,
                        principalTable: "School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", nullable: false),
                    DbSchoolSchoolId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.PersoonId);
                    table.ForeignKey(
                        name: "FK_Student_Persoon_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Persoon",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_School_DbSchoolSchoolId",
                        column: x => x.DbSchoolSchoolId,
                        principalTable: "School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DbStudentDbVak",
                columns: table => new
                {
                    StudentenPersoonId = table.Column<int>(type: "int", nullable: false),
                    VakkenVakId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbStudentDbVak", x => new { x.StudentenPersoonId, x.VakkenVakId });
                    table.ForeignKey(
                        name: "FK_DbStudentDbVak_Student_StudentenPersoonId",
                        column: x => x.StudentenPersoonId,
                        principalTable: "Student",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbStudentDbVak_Vak_VakkenVakId",
                        column: x => x.VakkenVakId,
                        principalTable: "Vak",
                        principalColumn: "VakId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbStudentDbVak_VakkenVakId",
                table: "DbStudentDbVak",
                column: "VakkenVakId");

            migrationBuilder.CreateIndex(
                name: "IX_Docent_DbSchoolSchoolId",
                table: "Docent",
                column: "DbSchoolSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_DbSchoolSchoolId",
                table: "Student",
                column: "DbSchoolSchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbStudentDbVak");

            migrationBuilder.DropTable(
                name: "Docent");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Vak");

            migrationBuilder.DropTable(
                name: "Persoon");

            migrationBuilder.DropTable(
                name: "School");
        }
    }
}
