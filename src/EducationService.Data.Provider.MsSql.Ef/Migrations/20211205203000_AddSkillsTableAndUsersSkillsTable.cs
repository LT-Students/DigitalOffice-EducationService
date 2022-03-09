using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.EducationService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(EducationServiceDbContext))]
  [Migration("20211205203000_AddSkillsTableAndUsersSkillsTable")]
  public class AddSkillsTableAndUsersSkillsTable : Migration
  {
    private void CreateUsersSkillsTable(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: "UsersSkills",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          UserId = table.Column<Guid>(nullable: false),
          SkillId = table.Column<Guid>(nullable: false),
          IsActive = table.Column<bool>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PR_UsersSkills", x => x.Id);
        });
    }

    private void CreateSkillsTable(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: "Skills",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false),
          IsActive = table.Column<bool>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Skills", x => x.Id);
        });
    }
    protected override void Up(MigrationBuilder builder)
    {
      CreateUsersSkillsTable(builder);
      CreateSkillsTable(builder);
    }

    protected override void Down(MigrationBuilder builder)
    {
      builder.DropTable(
        name: "UsersSkills");
      builder.DropTable(
        name: "Skills");
    }
  }
}
