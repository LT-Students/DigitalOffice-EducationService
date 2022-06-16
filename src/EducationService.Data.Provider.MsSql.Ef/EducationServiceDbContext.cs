using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Provider.MsSql.Ef
{
  public class EducationServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbUserEducation> UsersEducations { get; set; }
    public DbSet<DbEducationForm> EducationsForms { get; set; }
    public DbSet<DbEducationType> EducationsTypes { get; set; }
    public DbSet<DbEducationImage> EducationsImages { get; set; }

    public EducationServiceDbContext(DbContextOptions<EducationServiceDbContext> options)
      : base(options)
    {
    }

    // Fluent API is written here.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.EducationService.Models.Db"));
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;
      return Entry(obj).State;
    }

    async Task IBaseDataProvider.SaveAsync()
    {
      await SaveChangesAsync();
    }

    void IBaseDataProvider.Save()
    {
      SaveChanges();
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }
  }
}