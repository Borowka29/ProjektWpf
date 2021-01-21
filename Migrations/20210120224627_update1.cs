using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaZadan.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategoria_Zadanie_Kategorie_KategoriaIdKategoria",
                table: "Kategoria_Zadanie");

            migrationBuilder.DropForeignKey(
                name: "FK_Kategoria_Zadanie_Zadania_ZadanieIdZadanie",
                table: "Kategoria_Zadanie");

            migrationBuilder.AlterColumn<int>(
                name: "ZadanieIdZadanie",
                table: "Kategoria_Zadanie",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KategoriaIdKategoria",
                table: "Kategoria_Zadanie",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kategoria_Zadanie_Kategorie_KategoriaIdKategoria",
                table: "Kategoria_Zadanie",
                column: "KategoriaIdKategoria",
                principalTable: "Kategorie",
                principalColumn: "IdKategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kategoria_Zadanie_Zadania_ZadanieIdZadanie",
                table: "Kategoria_Zadanie",
                column: "ZadanieIdZadanie",
                principalTable: "Zadania",
                principalColumn: "IdZadanie",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategoria_Zadanie_Kategorie_KategoriaIdKategoria",
                table: "Kategoria_Zadanie");

            migrationBuilder.DropForeignKey(
                name: "FK_Kategoria_Zadanie_Zadania_ZadanieIdZadanie",
                table: "Kategoria_Zadanie");

            migrationBuilder.AlterColumn<int>(
                name: "ZadanieIdZadanie",
                table: "Kategoria_Zadanie",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KategoriaIdKategoria",
                table: "Kategoria_Zadanie",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategoria_Zadanie_Kategorie_KategoriaIdKategoria",
                table: "Kategoria_Zadanie",
                column: "KategoriaIdKategoria",
                principalTable: "Kategorie",
                principalColumn: "IdKategoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kategoria_Zadanie_Zadania_ZadanieIdZadanie",
                table: "Kategoria_Zadanie",
                column: "ZadanieIdZadanie",
                principalTable: "Zadania",
                principalColumn: "IdZadanie",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
