using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.EducationService.Models.Db
{
  public class DbEducationImage
  {
    public const string TableName = "EducationsImages";

    public Guid Id { get; set; }
    public Guid EducationId { get; set; }
    public Guid ImageId { get; set; }

    public DbUserEducation Education { get; set; }
  }

  public class DbCertificateImageConfiguration : IEntityTypeConfiguration<DbEducationImage>
  {
    public void Configure(EntityTypeBuilder<DbEducationImage> builder)
    {
      builder
        .ToTable(DbEducationImage.TableName);

      builder
        .HasKey(p => p.Id);

      builder
        .HasOne(pu => pu.Education)
        .WithMany(p => p.Images);
    }
  }
}
