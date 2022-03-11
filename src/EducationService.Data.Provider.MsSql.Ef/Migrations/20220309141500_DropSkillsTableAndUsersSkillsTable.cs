using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;


namespace LT.DigitalOffice.EducationService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(EducationServiceDbContext))]
  [Migration("20220309141500_DropSkillsTableAndUsersSkillsTable")]
  public class DropSkillsTableAndUsersSkillsTable : Migration
  {
    protected override void Up(MigrationBuilder builder)
    {
      builder.DropTable(
        name: "UsersSkills");

      builder.DropTable(
        name: "Skills");
    }
  }
}
