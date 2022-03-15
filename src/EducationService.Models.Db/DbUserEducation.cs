using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.EducationService.Models.Db
{
  public class DbUserEducation
  {
    public const string TableName = "UsersEducations";

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UniversityName { get; set; }
    public string QualificationName { get; set; }
    public Guid FormEducationId { get; set; }
    public Guid EducationtTypeId { get; set; }
    public int Сompleteness { get; set; }
    public DateTime AdmissionAt { get; set; }
    public DateTime? IssueAt { get; set; }
    public bool IsActive { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
    public DbEducationForm FormEducation { get; set; }
    public ICollection<DbEducationType> EducationType { get; set; }
  }



  public class DbUserEducationConfiguration : IEntityTypeConfiguration<DbUserEducation>
  {
    public void Configure(EntityTypeBuilder<DbUserEducation> builder)
    {
      builder
        .ToTable(DbUserEducation.TableName);

      builder
        .HasKey(e => e.Id);

      builder
        .Property(e => e.UniversityName)
        .IsRequired()
        .HasMaxLength(100); //?

      builder
        .Property(e => e.QualificationName)
        .IsRequired()
        .HasMaxLength(100); //?

      builder
        .HasOne(fe => fe.FormEducation)
        .WithMany(u => u.UsersEducation);
    }
  }
}
