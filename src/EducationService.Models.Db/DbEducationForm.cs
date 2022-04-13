using LT.DigitalOffice.Kernel.BrokerSupport.Attributes.ParseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Db
{
  [ParseEntity]
  public class DbEducationForm
  {
    public const string TableName = "EducationsForms";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }

    [IgnoreParse]
    public ICollection<DbUserEducation> UsersEducations { get; set; }

    public DbEducationForm()
    {
      UsersEducations = new HashSet<DbUserEducation>();
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
        .HasMany(u => u.UsersEducations)
        .WithOne(fe => fe.EducationForm);
    }
  }
}
