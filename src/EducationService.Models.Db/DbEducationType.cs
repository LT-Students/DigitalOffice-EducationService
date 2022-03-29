using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Db
{
  public class DbEducationType
  {
    public const string TableName = "EducationsTypes";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
    public ICollection<DbUserEducation> UsersEducation { get; set; }

    public DbEducationType()
    {
      UsersEducation = new HashSet<DbUserEducation>();
    }
  }

  public class DbEducationTypeConfiguration : IEntityTypeConfiguration<DbEducationType>
  {
    public void Configure(EntityTypeBuilder<DbEducationType> builder)
    {
      builder.ToTable(DbEducationType.TableName);

      builder.HasKey(x => x.Id);

      builder
        .Property(p => p.Name)
        .IsRequired();

      builder
        .HasMany(u => u.UsersEducation)
        .WithOne(fe => fe.EducationType);
    }
  }
}
