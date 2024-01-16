using Microsoft.EntityFrameworkCore;
using FinalProject;
using FinalProject.Server;
using FinalProject.Shared;

namespace FinalProject.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
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
                var connectionString = configuration.GetConnectionString("dbConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<Student> Students { get; set; }


    }
}
