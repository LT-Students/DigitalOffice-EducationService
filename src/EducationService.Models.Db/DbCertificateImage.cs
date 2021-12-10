using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.EducationService.Models.Db
{
  public class DbCertificateImage
  {
    public const string TableName = "CertificatesImages";

    public Guid Id { get; set; }
    public Guid CertificateId { get; set; }
    public Guid ImageId { get; set; }

    public DbUserCertificate Certificate { get; set; }
  }

  public class DbCertificateImageConfiguration : IEntityTypeConfiguration<DbCertificateImage>
  {
    public void Configure(EntityTypeBuilder<DbCertificateImage> builder)
    {
      builder
        .ToTable(DbCertificateImage.TableName);

      builder
        .HasKey(p => p.Id);

      builder
        .HasOne(pu => pu.Certificate)
        .WithMany(p => p.Images);
    }
  }
}
