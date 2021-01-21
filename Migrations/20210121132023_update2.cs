using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaZadan.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Podzadania_Zadania_ZadanieIdZadanie",
                table: "Podzadania");

            migrationBuilder.AlterColumn<int>(
                name: "ZadanieIdZadanie",
                table: "Podzadania",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Podzadania_Zadania_ZadanieIdZadanie",
                table: "Podzadania",
                column: "ZadanieIdZadanie",
                principalTable: "Zadania",
                principalColumn: "IdZadanie",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Podzadania_Zadania_ZadanieIdZadanie",
                table: "Podzadania");

            migrationBuilder.AlterColumn<int>(
                name: "ZadanieIdZadanie",
                table: "Podzadania",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Podzadania_Zadania_ZadanieIdZadanie",
                table: "Podzadania",
                column: "ZadanieIdZadanie",
                principalTable: "Zadania",
                principalColumn: "IdZadanie",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
