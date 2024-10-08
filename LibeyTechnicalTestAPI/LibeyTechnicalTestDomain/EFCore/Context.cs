using LibeyTechnicalTestDomain.EFCore.Configuration;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Domain;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
namespace LibeyTechnicalTestDomain.EFCore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LibeyUserConfiguration());
        }

        public DbSet<LibeyUser> LibeyUsers { get; set; }

        public DbSet<ILibeyProvince> Provinces { get; set; }
        public DbSet<ILibeyRegion> Regions { get; set; }
        public DbSet<ILibeyUbigeo> Ubigeos { get; set; }

    }
}