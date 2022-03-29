using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Db
{
  public class DbEducationForm
  {
    public const string TableName = "EducationsForms";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
    public ICollection<DbUserEducation> UsersEducation { get; set; }

    public DbEducationForm()
    {
      UsersEducation = new HashSet<DbUserEducation>();
    }
  }

  public class DbEducationFormConfiguration : IEntityTypeConfiguration<DbEducationForm>
  {
    public void Configure(EntityTypeBuilder<DbEducationForm> builder)
    {
      builder.ToTable(DbEducationForm.TableName);

      builder.HasKey(x => x.Id);

      builder
        .Property(p => p.Name)
        .IsRequired();

      builder
        .HasMany(u => u.UsersEducation)
        .WithOne(fe => fe.EducationForm);
    }
  }
}
