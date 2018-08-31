using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperSeller.Data.Migrations
{
    public partial class picturesAds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Ads_AdId",
                table: "Picture");

            migrationBuilder.AlterColumn<int>(
                name: "AdId",
                table: "Picture",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Ads_AdId",
                table: "Picture",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Ads_AdId",
                table: "Picture");

            migrationBuilder.AlterColumn<int>(
                name: "AdId",
                table: "Picture",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Ads_AdId",
                table: "Picture",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
