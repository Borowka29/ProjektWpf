using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaZadan.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    IdKategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.IdKategoria);
                });

            migrationBuilder.CreateTable(
                name: "Zadania",
                columns: table => new
                {
                    IdZadanie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prorytet = table.Column<double>(type: "float", nullable: false),
                    rozpoczecie = table.Column<DateTime>(type: "datetime2", nullable: true),
                    zakonczenie = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadania", x => x.IdZadanie);
                });

            migrationBuilder.CreateTable(
                name: "Kategoria_Zadanie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZadanieIdZadanie = table.Column<int>(type: "int", nullable: true),
                    KategoriaIdKategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria_Zadanie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategoria_Zadanie_Kategorie_KategoriaIdKategoria",
                        column: x => x.KategoriaIdKategoria,
                        principalTable: "Kategorie",
                        principalColumn: "IdKategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kategoria_Zadanie_Zadania_ZadanieIdZadanie",
                        column: x => x.ZadanieIdZadanie,
                        principalTable: "Zadania",
                        principalColumn: "IdZadanie",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Podzadania",
                columns: table => new
                {
                    IdPodzadania = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    któreNaLiscie = table.Column<int>(type: "int", nullable: true),
                    ZadanieIdZadanie = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podzadania", x => x.IdPodzadania);
                    table.ForeignKey(
                        name: "FK_Podzadania_Zadania_ZadanieIdZadanie",
                        column: x => x.ZadanieIdZadanie,
                        principalTable: "Zadania",
                        principalColumn: "IdZadanie",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategoria_Zadanie_KategoriaIdKategoria",
                table: "Kategoria_Zadanie",
                column: "KategoriaIdKategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Kategoria_Zadanie_ZadanieIdZadanie",
                table: "Kategoria_Zadanie",
                column: "ZadanieIdZadanie");

            migrationBuilder.CreateIndex(
                name: "IX_Podzadania_ZadanieIdZadanie",
                table: "Podzadania",
                column: "ZadanieIdZadanie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kategoria_Zadanie");

            migrationBuilder.DropTable(
                name: "Podzadania");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Zadania");
        }
    }
}
