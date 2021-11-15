using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.EducationService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(EducationServiceDbContext))]
  [Migration("20211115135000_InitialTables")]
  public class InitialTables : Migration
  {
    protected override void Up(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbUserCertificate.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          UserId = table.Column<Guid>(nullable: false),
          ImageId = table.Column<Guid>(nullable: false),
          EducationType = table.Column<int>(nullable: false),
          Name = table.Column<string>(nullable: false),
          SchoolName = table.Column<string>(nullable: false),
          IsActive = table.Column<bool>(nullable: false),
          ReceivedAt = table.Column<DateTime>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_UserCertificates", x => x.Id);
        });

      builder.CreateTable(
        name: DbUserEducation.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          UserId = table.Column<Guid>(nullable: false),
          UniversityName = table.Column<string>(nullable: false),
          QualificationName = table.Column<string>(nullable: false),
          FormEducation = table.Column<int>(nullable: false),
          AdmissionAt = table.Column<DateTime>(nullable: false),
          IssueAt = table.Column<DateTime>(nullable: true),
          IsActive = table.Column<bool>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_UserEducations", x => x.Id);
        });
    }
  }
}
