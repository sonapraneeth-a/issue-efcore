using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPSqlProvider.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ＭostExpensiveProducts",
                columns: table => new
                {
                    ＴenMostExpensiveProducts = table.Column<string>(nullable: false),
                    ＵnitPrice = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ＭostExpensiveProducts", x => x.ＴenMostExpensiveProducts);
                });

            migrationBuilder.CreateTable(
                name: "Ｐroducts",
                columns: table => new
                {
                    ＰroductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ＰroductName = table.Column<string>(nullable: true),
                    ＳupplierID = table.Column<int>(nullable: true),
                    ＵnitPrice = table.Column<decimal>(nullable: true),
                    ＵnitsInStock = table.Column<int>(nullable: false),
                    Ｄiscontinued = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ｐroducts", x => x.ＰroductID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ＭostExpensiveProducts");

            migrationBuilder.DropTable(
                name: "Ｐroducts");
        }
    }
}
