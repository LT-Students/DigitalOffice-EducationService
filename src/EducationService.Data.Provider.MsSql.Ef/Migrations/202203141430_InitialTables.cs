using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;


namespace LT.DigitalOffice.EducationService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(EducationServiceDbContext))]
  [Migration("202203141430_InitialTables")]
  public class InitialTables : Migration
  {
    public void CreateUserEducationTable(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbUserEducation.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          UserId = table.Column<Guid>(nullable: false),
          UniversityName = table.Column<string>(nullable: false),
          QualificationName = table.Column<string>(nullable: false),
          FormEducationId = table.Column<Guid>(nullable: false),
          EducationtTypeId = table.Column<Guid>(nullable: false),
          Сompleteness = table.Column<int>(nullable: false),
          AdmissionAt = table.Column<DateTime>(nullable: false),
          IssueAt = table.Column<DateTime>(nullable: true), //?
          IsActive = table.Column<bool>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PR_UsersEditions", x => x.Id);
        });
    }

    public void CreateEducationTypesandEducationFormsTable(MigrationBuilder builder)
    {

      builder.CreateTable(
        name: DbEducationType.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PR_EducationTypes", x => x.Id); //!!
        });

      builder.CreateTable(
      name: DbEducationForm.TableName,
      columns: table => new
      {
        Id = table.Column<Guid>(nullable: false),
        Name = table.Column<string>(nullable: false),
        CreatedBy = table.Column<Guid>(nullable: false),
        CreatedAtUtc = table.Column<DateTime>(nullable: false),
        ModifiedBy = table.Column<Guid>(nullable: true),
        ModifiedAtUtc = table.Column<DateTime>(nullable: true),
      },
      constraints: table =>
      {
        table.PrimaryKey("PR_EducationForms", x => x.Id); //!!
      });
    }
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      CreateUserEducationTable(migrationBuilder);
      CreateEducationTypesandEducationFormsTable(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
       name: DbUserEducation.TableName);

      migrationBuilder.DropTable(
       name: DbEducationForm.TableName);

      migrationBuilder.DropTable(
       name: DbEducationType.TableName);
    }
  }
}
