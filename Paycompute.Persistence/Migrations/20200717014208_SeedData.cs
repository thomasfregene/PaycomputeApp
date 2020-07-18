using Microsoft.EntityFrameworkCore.Migrations;

namespace Paycompute.Persistence.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE TaxYears SET YearOfTax = '2019/2020' WHERE Id = 1");
            migrationBuilder.Sql("UPDATE TaxYears SET YearOfTax = '2020/2021' WHERE Id = 2");
            migrationBuilder.Sql("UPDATE TaxYears SET YearOfTax = '2021/2022' WHERE Id = 3");
            migrationBuilder.Sql("UPDATE TaxYears SET YearOfTax = '2022/2023' WHERE Id = 4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TaxYears WHERE YearOfTax IN ('2019/2020', '2020/2021','2021/2022', '2022/2023')");
        }
    }
}
