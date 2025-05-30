using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Proxies;

namespace WebAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<Vacancy> Vacancies => Set<Vacancy>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Resume> Resumes => Set<Resume>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resume>()
               .HasKey(r => new { r.VacancyId, r.WorkerId });

            modelBuilder.Entity<Feedback>()
                .HasKey(f => new { f.WorkerId, f.CompanyId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Specify_Data_Source;Initial Catalog=DB;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True;").UseLazyLoadingProxies();
        }
    }
}
