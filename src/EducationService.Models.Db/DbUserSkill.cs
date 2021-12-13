﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.EducationService.Models.Db
{
  public class DbUserSkill
  {
    public const string TableName = "UsersSkills";

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public bool IsActive { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }

    public DbSkill Skill { get; set; }
  }

  public class DbUserSkillsConfiguration : IEntityTypeConfiguration<DbUserSkill>
  {
    public void Configure(EntityTypeBuilder<DbUserSkill> builder)
    {
      builder
        .ToTable(DbUserSkill.TableName);

      builder
        .HasKey(us => us.Id);

      builder
        .Property(us => us.UserId)
        .IsRequired();

      builder
        .Property(us => us.SkillId)
        .IsRequired();

      builder
        .HasOne(us => us.Skill)
        .WithMany(s => s.UsersSkill);
    }
  }
}