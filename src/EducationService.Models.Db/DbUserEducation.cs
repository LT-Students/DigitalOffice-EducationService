using System;
using System.Collections.Generic;
using LT.DigitalOffice.Kernel.BrokerSupport.Attributes.ParseEntity;
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
    public Guid EducationFormId { get; set; }
    public Guid EducationTypeId { get; set; }
    public int Completeness { get; set; }
    public DateTime AdmissionAt { get; set; }
    public DateTime? IssueAt { get; set; }
    public bool IsActive { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
    public DbEducationForm EducationForm { get; set; }
    public DbEducationType EducationType { get; set; }

    [IgnoreParse]
    public ICollection<DbEducationImage> Images { get; set; }

    public DbUserEducation()
    {
      Images = new HashSet<DbEducationImage>();
    }
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
        .HasMaxLength(100);

      builder
        .Property(e => e.QualificationName)
        .IsRequired()
        .HasMaxLength(100);

      builder
        .HasOne(fe => fe.EducationForm)
        .WithMany(u => u.UsersEducation);

      builder
        .HasOne(te => te.EducationType)
        .WithMany(u => u.UsersEducation);

      builder
       .HasMany(p => p.Images)
       .WithOne(tp => tp.Education);
    }
  }
}
