using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Models;
using System.Xml;

namespace PostgreSqlDotnetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() {}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("project");
            modelBuilder.Entity<UsersClass>().ToTable("users", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<VetCenter>().ToTable("vet_centers", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<BlogPostConsultation>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<RolesClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<CitiesClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<ProductsClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<DiagnosticsClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<OrdersClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Pet_StatusClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<BreedsClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<JobsClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<ManufacturersClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Pet_CaresClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<PetsClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<ReportsClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<TherapyClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Type_Of_PetsClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<MedecinesClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Pet_GaleryClass>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<BlogPostAnswers>().Metadata.SetIsTableExcludedFromMigrations(true);
            //modelBuilder.Entity<PetCareAllData>().Metadata.SetIsTableExcludedFromMigrations(true);
            base.OnModelCreating(modelBuilder);

            // ... model definition ...
        }

        public virtual DbSet<UsersClass> CustomerObj { get; set; }
        public virtual DbSet<VetCenter> VetCentersObj { get; set; }
        public virtual DbSet<BlogPostConsultation> BlogPostControllerObj { get; set; }
        public virtual DbSet<RolesClass> RoleControllerObj { get; set; }
        public virtual DbSet<CitiesClass> CitiesObj { get; set; }
        public virtual DbSet<ProductsClass> ProductObj { get; set; }
        public virtual DbSet<DiagnosticsClass> DiagObj { get; set; }
        public virtual DbSet<BlogPostAnswers> BlogPostAnswersObj { get; set; }
        public virtual DbSet<OrdersClass> OrderObj { get; set; }
        public virtual DbSet<Pet_StatusClass> PetStatusObj { get; set; }
        public virtual DbSet<BreedsClass> BreedsObj { get; set; }
        public virtual DbSet<JobsClass> JobsObj { get; set; }
        public virtual DbSet<ManufacturersClass> ManObj { get; set; }
        public virtual DbSet<MedecinesClass> MedeObj { get; set; }
        public virtual DbSet<Pet_CaresClass> PetCaresObj { get; set; }
        //public virtual DbSet<PetCareAllData> PetCaresWithAllDataObj { get; set; }
        public virtual DbSet<PetsClass> PetsObj { get; set; }
        public virtual DbSet<ReportsClass> repObj { get; set; }
        public virtual DbSet<TherapyClass> theraObj { get; set; }
        public virtual DbSet<Type_Of_PetsClass> typeofObj { get; set; }
    }
}