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
    public void CreateEducationImageTable(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbEducationImage.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          EducationId = table.Column<Guid>(nullable: false),
          ImageId = table.Column<Guid>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbEducationImage.TableName}", x => x.Id);
        });
    }

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
          EducationFormId = table.Column<Guid>(nullable: false),
          EducationTypeId = table.Column<Guid>(nullable: false),
          Completeness = table.Column<int>(nullable: false),
          AdmissionAt = table.Column<DateTime>(nullable: false),
          IssueAt = table.Column<DateTime>(nullable: true),
          IsActive = table.Column<bool>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PR_{DbUserEducation.TableName}", x => x.Id);
        });
    }

    public void CreateEducationTypesandEducationFormsTable(MigrationBuilder builder)
    {

      builder.CreateTable(
        name: DbEducationType.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false, maxLength: 100),
          CreatedBy = table.Column<Guid>(nullable: true),
          CreatedAtUtc = table.Column<DateTime>(nullable: true),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PR_{DbEducationType.TableName}", x => x.Id);
          table.UniqueConstraint($"UC_{DbEducationType.TableName}_Name_Unique", x => x.Name);
        });

      builder.CreateTable(
        name: DbEducationForm.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false, maxLength: 100),
          CreatedBy = table.Column<Guid>(nullable: true),
          CreatedAtUtc = table.Column<DateTime>(nullable: true),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PR_{DbEducationForm.TableName}", x => x.Id);
          table.UniqueConstraint($"UC_{DbEducationForm.TableName}_Name_Unique", x => x.Name);
        });
    }

    public void InsertData(MigrationBuilder builder)
    {
      builder.InsertData(
      table: DbEducationType.TableName,
      columns: new[] { "Id", "Name", "CreatedBy", "CreatedAtUtc" },
      columnTypes: new string[]
      {
        "uniqueidentifier",
        "nvarchar(max)",
        "uniqueidentifier",
        "datetime2"
      },
      values: new object[,]
      {
       { Guid.NewGuid(), "Higher Education", null, null },
       { Guid.NewGuid(), "Master's degree", null, null },
       { Guid.NewGuid(), "Specialized Secondary Education", null, null },
       { Guid.NewGuid(), "Refresher course", null, null }
      });

     builder.InsertData(
     table: DbEducationForm.TableName,
     columns: new[] { "Id", "Name", "CreatedBy", "CreatedAtUtc" },
     columnTypes: new string[]
     {
        "uniqueidentifier",
        "nvarchar(max)",
        "uniqueidentifier",
        "datetime2"
     },
     values: new object[,]
     {
       { Guid.NewGuid(), "Online", null, null },
       { Guid.NewGuid(), "Offline", null, null },
       { Guid.NewGuid(), "Part-time", null, null },
       { Guid.NewGuid(), "Full-time", null, null }
     });
    }

    protected override void Up(MigrationBuilder migrationBuilder)
    {
      CreateUserEducationTable(migrationBuilder);
      CreateEducationTypesandEducationFormsTable(migrationBuilder);
      CreateEducationImageTable(migrationBuilder);
      InsertData(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
       name: DbUserEducation.TableName);

      migrationBuilder.DropTable(
       name: DbEducationForm.TableName);

      migrationBuilder.DropTable(
       name: DbEducationForm.TableName);

      migrationBuilder.DropTable(
       name: DbEducationImage.TableName);
    }
  }
}
